using System;
using System.Collections.Generic;

namespace Gone.Tests
{
    public class Game : IGame
    {
        private readonly IEnumerable<Player> _players;
        private readonly IGrid _grid = new Grid();
        private readonly TimeSpan _gameTime;

        public Game(IEnumerable<Player> players, TimeSpan gameTime)
        {
            _players = players;
            _gameTime = gameTime;
            _grid.InitializeWith(_players);
        }

        public IGame Start()
        {
            throw new NotImplementedException();
        }
    }
}