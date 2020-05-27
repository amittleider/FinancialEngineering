using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialEngineering
{
    public class FixedIncomeCalculator
    {
        public double PresentValueConstantCashFlow(double interestRate, params double[] cashFlows)
        {
            var r = interestRate;
            var c = cashFlows;
            double presentValue = 0;
            for (int i = 0; i < c.Length; i++)
            {
                presentValue += c[i] / Math.Pow(1 + r, i);
            }

            return presentValue;
        }

        public double PresentValuePerpetuity(double amountPerPayment, double interestRate)
        {
            return amountPerPayment / interestRate;
        }

        public double PresentValueAnnuity(double amountPerPayment, double interestRate, int periods)
        {
            return amountPerPayment / interestRate * (1 - 1 / Math.Pow((1 + interestRate), periods));
        }
    }
}
