using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FinancialEngineering.Tests
{
    public class ZeroCouponBondTests
    {
        [Fact]
        public void Should_PriceZcb()
        {
            ZeroCouponBond zeroCouponBond = new ZeroCouponBond(0.06, 1.25, 0.9, 0.5);
            double[,] price = zeroCouponBond.Price(100, 4);

            price[4, 4].Should().BeApproximately(100.0, 0.01);
            price[4, 3].Should().BeApproximately(100.0, 0.01);
            price[4, 2].Should().BeApproximately(100.0, 0.01);
            price[4, 1].Should().BeApproximately(100.0, 0.01);
            price[4, 0].Should().BeApproximately(100.0, 0.01);

            price[3, 3].Should().BeApproximately(89.51, 0.01);
            price[3, 2].Should().BeApproximately(92.22, 0.01);
            price[3, 1].Should().BeApproximately(94.27, 0.01);
            price[3, 0].Should().BeApproximately(95.81, 0.01);

            price[2, 2].Should().BeApproximately(83.08, 0.01);
            price[2, 1].Should().BeApproximately(87.35, 0.01);
            price[2, 0].Should().BeApproximately(90.64, 0.01);

            price[1, 1].Should().BeApproximately(79.27, 0.01);
            price[1, 0].Should().BeApproximately(84.43, 0.01);

            price[0, 0].Should().BeApproximately(77.22, 0.01);
        }

        [Fact]
        public void Should_PriceZcb2()
        {
            ZeroCouponBond zeroCouponBond = new ZeroCouponBond(0.04, 1.1, 0.8, 0.6);
            double[,] price = zeroCouponBond.Price(200, 4);

            price[4, 4].Should().BeApproximately(200.0, 0.01);
            price[4, 3].Should().BeApproximately(200.0, 0.01);
            price[4, 2].Should().BeApproximately(200.0, 0.01);
            price[4, 1].Should().BeApproximately(200.0, 0.01);
            price[4, 0].Should().BeApproximately(200.0, 0.01);

            price[3, 3].Should().BeApproximately(189.89, 0.01);
            price[3, 2].Should().BeApproximately(192.54, 0.01);
            price[3, 1].Should().BeApproximately(194.52, 0.01);
            price[3, 0].Should().BeApproximately(195.99, 0.01);

            price[2, 2].Should().BeApproximately(182.14, 0.01);
            price[2, 1].Should().BeApproximately(186.76, 0.01);
            price[2, 0].Should().BeApproximately(190.24, 0.01);

            price[1, 1].Should().BeApproximately(176.23, 0.01);
            price[1, 0].Should().BeApproximately(182.32, 0.01);

            price[0, 0].Should().BeApproximately(171.79, 0.01);
        }
    }
}
