using FinancialEngineering.Enums;
using FluentAssertions;
using Xunit;

namespace FinancialEngineering.Tests
{
    public class OptionTests
    {
        [Fact]
        public void Should_PriceEuropeanCall()
        {
            Option option = new Option(100, 1.01, 3, 1.07, 0.93458, 0.557, PutCallType.Call, AmericanEuropeanType.European);
            double[,] price = option.Price(100);

            price[0, 0].Should().BeApproximately(6.576, 0.001);
            price[1, 0].Should().BeApproximately(2.129, 0.001);
            price[2, 0].Should().BeApproximately(0, 0.001);
            price[3, 0].Should().BeApproximately(0, 0.001);

            price[1, 1].Should().BeApproximately(10.231, 0.001);
            price[2, 1].Should().BeApproximately(3.86, 0.001);
            price[3, 1].Should().BeApproximately(0, 0.001);

            price[2, 2].Should().BeApproximately(15.481, 0.001);
            price[3, 2].Should().BeApproximately(7, 0.001);

            price[3, 3].Should().BeApproximately(22.504, 0.001);
        }
    }
}
