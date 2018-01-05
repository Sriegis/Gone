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
        public void GameTurn_Should_NotTakeMoreThanHalfASec()
        {
            var strategy = new Mock<IStrategy>();
            strategy.Setup(s => s.Turn(It.IsAny<MyCell[]>()))
                .Callback(() => Task.Delay(600))
                .Returns(new Transaction(Guid.NewGuid(), Guid.NewGuid(), 10));

            var players = Enumerable.Range(1, 5).Select(i => new Player
            {
                Id = Guid.NewGuid(),
                Name = $"Name{i}",
                Strategy = strategy.Object
            });

            var gameTime = new TimeSpan(0, 0, 1);

            var game = new Game(players, gameTime);

            Assert.Throws<TimeoutException>(() => game.Start());
        }
    }
}