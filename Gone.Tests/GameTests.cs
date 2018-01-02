using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Gone.Tests
{
    public class GameTests
    {
        [Fact]
        public void TransactionProcessing_Should_NotTakeMoreThanHalfASecond()
        {
            var players = new Mock<IList<Player>>();

            var strategies = new Mock<IStrategy>();
            strategies.

            players.Setup(l => l.Select(It.IsAny<Func<Player, bool>>())).Returns(strategies);

            var players = Enumerable.Range(1, 5).Select(i => new Player
            {
                Name = $"Name{i}",
                Strategy = new StrategyStub()
            });

            var game = new Game(players);

            game.Start();


        }
    }

    public interface IGame
    {
        IGame Start();
    }

    public class Game : IGame
    {
        private readonly IEnumerable<Player> _players;
        private readonly IGrid _grid = new Grid();

        public Game(IEnumerable<Player> players)
        {
            _players = players;
            _grid.InitializeWith(_players);
        }

        public IGame Start()
        {
            throw new NotImplementedException();
        }
    }
}