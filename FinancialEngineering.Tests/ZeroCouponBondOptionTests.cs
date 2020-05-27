using FinancialEngineering.Enums;
using FluentAssertions;
using Xunit;

namespace FinancialEngineering.Tests
{
    public class ZeroCouponBondOptionTests
    {
        [Fact]
        public void Should_PriceZeroCouponBond_EuropeanCall()
        {
            ZeroCouponBondOption zeroCouponBondOption = new ZeroCouponBondOption(0.06, 1.25, 0.9, 0.5, 2, 84.0, PutCallType.Call, AmericanEuropeanType.European);
            double[,] price = zeroCouponBondOption.Price(100, 4);

            price[2, 2].Should().BeApproximately(0.0, 0.01);
            price[2, 1].Should().BeApproximately(3.35, 0.01);
            price[2, 0].Should().BeApproximately(6.64, 0.01);

            price[1, 1].Should().BeApproximately(1.56, 0.01);
            price[1, 0].Should().BeApproximately(4.74, 0.01);

            price[0, 0].Should().BeApproximately(2.97, 0.01);
        }

        [Fact]
        public void Should_PriceZeroCouponBond_AmericanPut()
        {
            ZeroCouponBondOption zeroCouponBondOption = new ZeroCouponBondOption(0.06, 1.25, 0.9, 0.5, 3, 88.0, PutCallType.Put, AmericanEuropeanType.American);
            double[,] price = zeroCouponBondOption.Price(100, 4);

            price[3, 3].Should().BeApproximately(0, 0.01);
            price[3, 2].Should().BeApproximately(0, 0.01);
            price[3, 1].Should().BeApproximately(0, 0.01);
            price[3, 0].Should().BeApproximately(0, 0.01);

            price[2, 2].Should().BeApproximately(4.92, 0.01);
            price[2, 1].Should().BeApproximately(0.65, 0.01);
            price[2, 0].Should().BeApproximately(0, 0.01);

            price[1, 1].Should().BeApproximately(8.73, 0.01);
            price[1, 0].Should().BeApproximately(3.57, 0.01);

            price[0, 0].Should().BeApproximately(10.78, 0.01);
        }
    }
}
