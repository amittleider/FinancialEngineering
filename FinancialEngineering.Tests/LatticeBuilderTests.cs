using FluentAssertions;
using System;
using Xunit;

namespace FinancialEngineering.Tests
{
    public class LatticeBuilderTests
    {
        [Fact]
        public void Should_ConstructShortRateLattice()
        {
            double r = 6 / 100.0;
            double u = 1.25;
            double d = 0.9;

            var shortRateLattice = LatticeBuilder.ConstructLattice(r, u, d, 5);

            shortRateLattice[0, 0].Should().BeApproximately(0.06, 0.001);
            shortRateLattice[1, 0].Should().BeApproximately(0.054, 0.001);
            shortRateLattice[2, 0].Should().BeApproximately(0.0486, 0.001);
            shortRateLattice[3, 0].Should().BeApproximately(0.0437, 0.001);
            shortRateLattice[4, 0].Should().BeApproximately(0.0394, 0.001);
            shortRateLattice[5, 0].Should().BeApproximately(0.0354, 0.001);

            shortRateLattice[1, 1].Should().BeApproximately(0.075, 0.001);
            shortRateLattice[2, 1].Should().BeApproximately(0.0675, 0.001);
            shortRateLattice[3, 1].Should().BeApproximately(0.0608, 0.001);
            shortRateLattice[4, 1].Should().BeApproximately(0.0547, 0.001);
            shortRateLattice[5, 1].Should().BeApproximately(0.0492, 0.001);

            shortRateLattice[2, 2].Should().BeApproximately(0.0938, 0.001);
            shortRateLattice[3, 2].Should().BeApproximately(0.0844, 0.001);
            shortRateLattice[4, 2].Should().BeApproximately(0.0759, 0.001);
            shortRateLattice[5, 2].Should().BeApproximately(0.0683, 0.001);

            shortRateLattice[3, 3].Should().BeApproximately(0.1172, 0.001);
            shortRateLattice[4, 3].Should().BeApproximately(0.1055, 0.001);
            shortRateLattice[5, 3].Should().BeApproximately(0.0949, 0.001);

            shortRateLattice[4, 4].Should().BeApproximately(0.1465, 0.001);
            shortRateLattice[5, 4].Should().BeApproximately(0.1318, 0.001);

            shortRateLattice[5, 5].Should().BeApproximately(0.1831, 0.001);
        }

        [Fact]
        public void Should_ConstructStockLattice()
        {
            double u = 1.07;
            double d = 0.93458;

            var stockLattice = LatticeBuilder.ConstructLattice(100, u, d, 3);

            stockLattice[0, 0].Should().BeApproximately(100, 0.001);
            stockLattice[1, 0].Should().BeApproximately(93.458, 0.001);
            stockLattice[2, 0].Should().BeApproximately(87.344, 0.001);
            stockLattice[3, 0].Should().BeApproximately(81.630, 0.001);

            stockLattice[1, 1].Should().BeApproximately(107, 0.001);
            stockLattice[2, 1].Should().BeApproximately(100, 0.001);
            stockLattice[3, 1].Should().BeApproximately(93.458, 0.001);

            stockLattice[2, 2].Should().BeApproximately(114.49, 0.001);
            stockLattice[3, 2].Should().BeApproximately(107, 0.001);

            stockLattice[3, 3].Should().BeApproximately(122.504, 0.001);
        }
    }
}
