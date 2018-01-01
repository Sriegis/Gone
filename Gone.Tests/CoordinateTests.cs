using System;
using Xunit;

namespace Gone.Tests
{
    public class CoordinateTests
    {
        [Fact]
        public void Coordinates_Should_CalculateDistanceCorrectly()
        {
            var playerCount = 20;
            var cellGenerator = new CellGenerator(playerCount);
            var cells = cellGenerator.Generate();

            var rng = new Random();
            var random1 = rng.Next(cells.Length - 1);
            var random2 = rng.Next(cells.Length - 1);

            var c1 = cells[random1].Coordinates;
            var c2 = cells[random2].Coordinates;

            var expectedDistance = 0.5d * (Math.Abs(c1.X - c2.X)
                + Math.Abs(c1.Y - c2.Y)
                + Math.Abs(c1.Z - c2.Z));

            var actualDistance = Coordinates.GetDistance(c1, c2);

            Assert.Equal(expectedDistance, actualDistance);
        }
    }
}
