using FinancialEngineering.Enums;
using System;

namespace FinancialEngineering
{
    public class ZeroCouponBondOption
    {
        private ZeroCouponBond zcb; // The zero coupon bond associated with this option
        private double rate;
        private double u;
        private double d;
        private double qu;
        private double qd;
        private int expirationPeriods; // The number of periods before expiration
        private double strike; // Strike price
        private PutCallType putCallType; // Put/call
        private AmericanEuropeanType americanEuropeanType; // American/european

        public ZeroCouponBondOption(double rate, double u, double d, double qu, int expirationPeriods, double strike, PutCallType putCallType, AmericanEuropeanType americanEuropeanType)
        {
            this.zcb = new ZeroCouponBond(rate, u, d, qu);
            this.rate = rate;
            this.u = u;
            this.d = d;
            this.qu = qu;
            this.qd = 1 - qu;
            this.expirationPeriods = expirationPeriods;
            this.strike = strike;
            this.putCallType = putCallType;
            this.americanEuropeanType = americanEuropeanType;
        }

        public double[,] Price(double faceValue, int periods)
        {
            double[,] shortRateLattice = LatticeBuilder.ConstructLattice(this.rate, this.u, this.d, periods);

            var zcbPrice = this.zcb.Price(faceValue, periods);

            var optionPrice = new double[expirationPeriods + 1, expirationPeriods + 1];

            Func<double, double, double> putCallFunction = delegate (double strike, double price)
            {
                if (putCallType == PutCallType.Call)
                {
                    return Math.Max(0, price - strike);
                }

                return Math.Max(0, strike - price);
            };

            Func<double, double, double> americanEuropeanFunction = delegate (double optionValue, double earlyExercizeValue)
            {
                if (americanEuropeanType == AmericanEuropeanType.European)
                {
                    return optionValue;
                }

                return Math.Max(optionValue, earlyExercizeValue); // American type
            };

            // Initialize the expiry
            for (int j = 0; j <= expirationPeriods; j++)
            {
                optionPrice[expirationPeriods, j] = putCallFunction(strike, zcbPrice[expirationPeriods, j]);
            }

            double[,] r = shortRateLattice;
            double[,] z = optionPrice;

            // Fill the rest of the matrix
            for (int i = expirationPeriods - 1; i >= 0; i--)
            {
                for (int j = i; j >= 0; j--)
                {
                    var optionValue = 1 / (1 + r[i, j]) * (qu * z[i + 1, j + 1] + qd * z[i + 1, j]);
                    var earlyExercizeValue = putCallFunction(this.strike, zcbPrice[i, j]);
                    z[i, j] = americanEuropeanFunction(optionValue, earlyExercizeValue);
                }
            }

            return z;
        }
    }
}
