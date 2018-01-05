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
                Id = Guid.NewGuid(),
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

            var actualPositions = grid.Cells.Where(c => c.CellOwner != null).Select(c => c.Coordinates);

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
                Id = Guid.NewGuid(),
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

            var actualPositions = grid.Cells.Where(c => c.CellOwner != null).Select(c => c.Coordinates);

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
                Id = Guid.NewGuid(),
                Name = $"Name{i}",
                Strategy = new StrategyStub()
            });

            var grid = new Grid();

            grid.InitializeWith(players);

            var actualPositions = grid.Cells.Where(c => c.CellOwner != null);

            Assert.Equal(stratCount, actualPositions.Count());
        }

        [Fact]
        public void GridGetPlayerCells_Should_ReturnTheCellsOfThePlayer()
        {
            var players = Enumerable.Range(1, 15).Select(i => new Player
            {
                Id = Guid.NewGuid(),
                Name = $"Name{i}",
                Strategy = new StrategyStub()
            });

            var thatParticularPlayer = new Player
            {
                Id = Guid.Parse("00000000-1111-2222-3333-444444444444"),
                Name = "Linas",
                Strategy = new StrategyStub()
            };

            var grid = new Grid();

            grid.InitializeWith(players);

            var actualCells = grid.Cells.Take(3).ToList();

            actualCells.ForEach(c => c.CellOwner = thatParticularPlayer);

            var expectedCells = grid.GetPlayerCells(thatParticularPlayer);

            Assert.Equal(expectedCells, actualCells);
        }
    }
}