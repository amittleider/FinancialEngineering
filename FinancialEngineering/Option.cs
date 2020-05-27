using FinancialEngineering.Enums;
using System;

namespace FinancialEngineering
{
    public class Option
    {
        private double currentPrice;
        private double riskFreeRate;
        private int expirationPeriods;
        private double u;
        private double d;
        private double qu;
        private double qd;
        private PutCallType putCallType;
        private AmericanEuropeanType americanEuropeanType;

        public Option(double currentPrice, double riskFreeRate, int expirationPeriods, double u, double d, double qu, PutCallType putCallType, AmericanEuropeanType americanEuropeanType)
        {
            this.currentPrice = currentPrice;
            this.riskFreeRate = riskFreeRate;
            this.expirationPeriods = expirationPeriods;
            this.u = u;
            this.d = d;
            this.qu = qu;
            this.qd = 1 - qu;
            this.putCallType = putCallType;
            this.americanEuropeanType = americanEuropeanType;
        }

        public double[,] Price(double strike)
        {
            double[,] stockPrice = LatticeBuilder.ConstructLattice(this.currentPrice, this.u, this.d, this.expirationPeriods);

            var optionPrice = new double[expirationPeriods + 1, expirationPeriods + 1];

            Func<double, double, double> putCallFunction = delegate (double _strike, double _price)
            {
                if (putCallType == PutCallType.Call)
                {
                    return Math.Max(0, _price - _strike);
                }

                return Math.Max(0, _strike - _price);
            };

            Func<double, double, double> americanEuropeanFunction = delegate (double _optionValue, double _earlyExerciseValue)
            {
                if (americanEuropeanType == AmericanEuropeanType.European)
                {
                    return _optionValue;
                }

                return Math.Max(_optionValue, _earlyExerciseValue); // American type
            };

            // Initialize the expiry
            for (int j = 0; j <= expirationPeriods; j++)
            {
                optionPrice[expirationPeriods, j] = putCallFunction(strike, stockPrice[expirationPeriods, j]);
            }

            double[,] c = optionPrice;

            // Fill the rest of the matrix
            for (int i = expirationPeriods - 1; i >= 0; i--)
            {
                for (int j = i; j >= 0; j--)
                {
                    var optionValue = (1 / riskFreeRate) * (qu * c[i + 1, j + 1] + qd * c[i + 1, j]);
                    var earlyExerciseValue = putCallFunction(strike, stockPrice[i, j]);
                    c[i, j] = americanEuropeanFunction(optionValue, earlyExerciseValue);
                }
            }

            return c;
        }
    }
}
