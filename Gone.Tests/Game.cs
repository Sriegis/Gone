using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Gone.Tests
{
    public class Game : IGame
    {
        private readonly IEnumerable<Player> _players;
        private readonly IGrid _grid = new Grid();
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly TimeSpan _gameTime;

        public Game(IEnumerable<Player> players, TimeSpan gameTime)
        {
            _players = players;
            _stopwatch.Reset();
            _gameTime = gameTime;
            _grid.InitializeWith(_players);
        }

        public void Start()
        {
            Run();
        }

        private void Run()
        {
            _stopwatch.Start();

            while (_stopwatch.ElapsedMilliseconds < _gameTime.TotalMilliseconds)
            {
                ProcessTurns();
            }

            _stopwatch.Stop();
        }

        private void ProcessTurns()
        {
            
        }
    }
}