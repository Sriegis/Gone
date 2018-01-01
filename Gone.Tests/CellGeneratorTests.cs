using System;
using System.Linq;
using Xunit;

namespace Gone.Tests
{
    public class CellGeneratorTests
    {
        [Fact]
        public void CellGenerator_Should_GenerateCells()
        {
            var playerCount = 2;
            var generator = new CellGenerator(playerCount);
            var cells = generator.Generate();
            Assert.True(cells.GetType() == typeof(Cell[]));
        }

        [Fact]
        public void CellGenerator_Should_GenerateUniqueCells()
        {
            var playerCount = 2;
            var generator = new CellGenerator(playerCount);
            var cells = generator.Generate();
            Assert.Equal(cells.Length, cells.Distinct().Count());
        }

        [Fact]
        public void CellGenerator_Should_GenerateCellsWithUniqueCoordinates()
        {
            var playerCount = 3;
            var generator = new CellGenerator(playerCount);
            var cells = generator.Generate();
            Assert.Equal(cells.Length, cells.Select(x => x.Coordinates).Distinct().Count());
        }

        [Theory]
        [InlineData(5, 127)]
        [InlineData(13, 331)]
        [InlineData(26, 631)]
        [InlineData(52, 1027)]
        public void CellGenerator_Should_GenerateACorrectNumberOfCells(int playerCount, int expectedCellCount)
        {
            var generator = new CellGenerator(playerCount);
            var cells = generator.Generate();
            Assert.Equal(expectedCellCount, cells.Length);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(7)]
        public void CellGenerator_Should_GenerateACorrectNumberOfCellsForAParticularShell(int shell)
        {
            var playerCount = 20;
            var cellGenerator = new CellGenerator(playerCount);
            var cells = cellGenerator.Generate();

            var actualShellCount = cells.Count(c => (
                Math.Abs(c.Coordinates.X) + 
                Math.Abs(c.Coordinates.Y) + 
                Math.Abs(c.Coordinates.Z)) * 0.5d == shell);

            var expected = shell == 0 
                ? 1 
                : shell * 6;

            Assert.Equal(expected, actualShellCount);
        }

        [Fact]
        public void CellGeneratorCellCoordinates_Should_AddToZero()
        {
            var playerCount = 20;
            var cellGenerator = new CellGenerator(playerCount);
            var cells = cellGenerator.Generate();
            Assert.True(cells.All(c => c.Coordinates.X + c.Coordinates.Y + c.Coordinates.Z == 0));
        }

        [Fact]
        public void CellGeneratorMaxOrdinate_Should_MatchShellCount()
        {
            var playerCount = 52;
            var cellGenerator = new CellGenerator(playerCount);
            var cells = cellGenerator.Generate();

            var maxShell = cells.Max(c => Coordinates.GetDistance(c.Coordinates, Coordinates.Origin));

            var maxX = cells.Max(c => c.Coordinates.X);
            var maxY = cells.Max(c => c.Coordinates.Y);
            var maxZ = cells.Max(c => c.Coordinates.Z);

            var minX = cells.Min(c => c.Coordinates.X);
            var minY = cells.Min(c => c.Coordinates.Y);
            var minZ = cells.Min(c => c.Coordinates.Z);

            Assert.Equal(maxShell, maxX);
            Assert.Equal(maxShell, maxY);
            Assert.Equal(maxShell, maxZ);
            Assert.Equal(-maxShell, minX);
            Assert.Equal(-maxShell, minY);
            Assert.Equal(-maxShell, minZ);
        }
    }
}
