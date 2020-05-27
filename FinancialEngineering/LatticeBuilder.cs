using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

namespace FinancialEngineering
{
    public class LatticeBuilder
    {
        public static double[,] ConstructLattice(double initialValue, double u, double d, int periods)
        {
            double[,] shortRateLattice = new double[periods + 1, periods + 1];
            shortRateLattice[0, 0] = initialValue;

            // Initialize the up-moves
            for (int i = 1; i <= periods; i++)
            {
                shortRateLattice[i, i] = shortRateLattice[i - 1, i - 1] * u;
            }

            // Initialize the down-moves
            for (int j = 0; j <= periods; j++)
            {
                for (int i = j + 1; i <= periods; i++)
                {
                    shortRateLattice[i, j] = shortRateLattice[i - 1, j] * d;
                }
            }

            return shortRateLattice;
        }
    }
}