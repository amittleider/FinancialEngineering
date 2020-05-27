using System;

namespace FinancialEngineering
{
    public class ZeroCouponBond
    {
        double rate; // The interest rate
        double u; // Upward difference of an up price move
        double d; // Downward difference of a down price move
        double qu; // Upward price move probability
        double qd; // Downward price move probability

        public ZeroCouponBond(double rate, double u, double d, double qu)
        {
            this.rate = rate;
            this.u = u;
            this.d = d;
            this.qu = qu;
            this.qd = 1 - qu;
        }

        public double[,] Price(double faceValue, int periods)
        {
            double[,] shortRateLattice = LatticeBuilder.ConstructLattice(this.rate, this.u, this.d, periods);

            double[,] bondPriceLattice = new double[periods + 1, periods + 1];

            // Initialize the face values
            for (int j = 0; j <= periods; j++)
            {
                bondPriceLattice[periods, j] = faceValue;
            }

            // Fill the rest of the matrix
            double[,] r = shortRateLattice;
            double[,] z = bondPriceLattice;

            for (int i = periods - 1; i >= 0; i--)
            {
                for (int j = i; j >= 0; j--)
                {
                    z[i, j] = 1 / (1 + r[i, j]) * (qu * z[i + 1, j + 1] + qd * z[i + 1, j]);
                }
            }

            return z;
        }
    }
}
