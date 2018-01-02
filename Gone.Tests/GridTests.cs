using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Linq;

namespace Gone.Tests
{
    public class GridTests
    {
        [Fact]
        public void Grid_Should_SetUpInitialPlayerPositions()
        {
            var strats = 8;

            var players = Enumerable.Range(1, strats).Select(i => new Player
            {
                Name = $"Name{i}",
                Strategy = new StrategyStub()
            });

            var grid = new Grid();

            grid.InitializeWith(players);

            var positions = new List<Coordinates>
            {
                new Coordinates(0, 0, 0),
                new Coordinates(-4, 0, 4),
                new Coordinates(-4, 4, 0),
                new Coordinates(0, 4, -4),
                new Coordinates(4, 0, -4),
                new Coordinates(4, -4, 0),
                new Coordinates(0, -4, 4),
                new Coordinates(-8, 0, 8)
            };

            var actualPositions = grid.Cells.Where(c => c.CellOwner != "None").Select(c => c.Coordinates);

            Assert.True(actualPositions.SequenceEqual(actualPositions));
            Assert.True(actualPositions.All(a => positions.Any(p => p.Equals(a))));
            Assert.Equal(positions.Count, actualPositions.Count());
        }

        [Fact]
        public void Grid_Should_SetUpInitialPlayerPositionsForUnfilledPlayers()
        {
            var strats = 4;

            var players = Enumerable.Range(1, strats).Select(i => new Player
            {
                Name = $"Name{i}",
                Strategy = new StrategyStub()
            });

            var grid = new Grid();

            grid.InitializeWith(players);

            var positions = new List<Coordinates>
            {
                new Coordinates(0, 0, 0),
                new Coordinates(-4, 0, 4),
                new Coordinates(-4, 4, 0),
                new Coordinates(0, 4, -4)
            };

            var actualPositions = grid.Cells.Where(c => c.CellOwner != "None").Select(c => c.Coordinates);

            Assert.True(actualPositions.SequenceEqual(actualPositions));
            Assert.True(actualPositions.All(a => positions.Any(p => p.Equals(a))));
            Assert.Equal(positions.Count, actualPositions.Count());
        }

        [Theory]
        [InlineData(17)]
        [InlineData(52)]
        public void Grid_Should_AssignTheActualNumberOfCellsToPlayersAsThereArePlayers(int stratCount)
        {
            var players = Enumerable.Range(1, stratCount).Select(i => new Player
            {
                Name = $"Name{i}",
                Strategy = new StrategyStub()
            });

            var grid = new Grid();

            grid.InitializeWith(players);

            var actualPositions = grid.Cells.Where(c => c.CellOwner != "None");

            Assert.Equal(stratCount, actualPositions.Count());
        }

        //[Fact]
        //public void Grid_Should_ProcessAllGivenTransactions()
        //{
        //    var grid = new Mock<IGrid>();

        //    var trans = new List<Transaction>();
        //    trans.Add(new Transaction());
        //    trans.Add(new Transaction());
        //    trans.Add(new Transaction());

        //    grid.Setup(g => g.ProcessTransactions(trans));
        //}
    }
}