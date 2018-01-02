using System;
using System.Collections.Generic;
using System.Linq;
using Gone.Extensions;

namespace Gone
{
    public class Grid : IGrid
    {
        public IEnumerable<ICell> Cells { get; set; }

        public Grid InitializeWith(IEnumerable<Player> players)
        {
            var cellGenerator = new CellGenerator(players.Count());
            Cells = cellGenerator.Generate();
            AssignStartingPlayerCells(players);
            return this;
        }

        public void ProcessTransactions(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                Process(transaction);
            }
        }

        private void Process(Transaction transaction)
        {
            
        }

        private void AssignStartingPlayerCells(IEnumerable<Player> players)
        {
            var coordinates = GetPlayerCoordinates()
                .Take(players.Count())
                .ToList();

            var playerNames = players.Select(p => p.Name).OrderBy(_ => Guid.NewGuid());

            var playerNameQueue = new Queue<string>(playerNames);

            foreach (var coordinate in coordinates)
            {
                var cell = Cells.First(c => c.Coordinates.Equals(coordinate));
                cell.CellOwner = playerNameQueue.Dequeue();
            }
        }

        private IEnumerable<Coordinates> GetPlayerCoordinates()
        {
            var coordinates = new List<Coordinates>();

            var maxPlayerShell = GetMaximumsNumberOfShells();

            for (var n = 0; 4 * n + 1 <= maxPlayerShell; n++)
            {
                var shell = 4 * n + 1;

                var coordinateMover = new PlayerCoordinateNavigator
                {
                    StartingPoint = new Coordinates(-shell + 1, 0, shell - 1),
                    Directions = GetDirections(n),
                    TimesToMove = n
                };

                var calculatedCoordinates = MoveNTimesAndReturnCoordinates(coordinateMover);

                coordinates.AddRange(calculatedCoordinates);
            }

            return coordinates;
        }

        private IEnumerable<Coordinates> MoveNTimesAndReturnCoordinates(PlayerCoordinateNavigator coordinateNavigator)
        {
            var coordinates = new List<Coordinates>();

            var currentCoordinates = coordinateNavigator.StartingPoint;

            foreach (var direction in coordinateNavigator.Directions)
            {
                coordinates.Add(currentCoordinates);
                currentCoordinates = MoveADirectionAndGetCoordinates(currentCoordinates, direction);
            }

            return coordinates;
        }

        private Coordinates MoveADirectionAndGetCoordinates(Coordinates startingPoint, Coordinates direction)
        {
            return startingPoint + direction;
        }

        private IEnumerable<Coordinates> GetDirections(int timesToMove)
        {
            if (timesToMove == 0)
                return new List<Coordinates> { Coordinates.Origin };

            var directions = GetDirections().Nplicate(timesToMove - 1);

            return directions;
        }

        private IEnumerable<Coordinates> GetDirections()
        {
            yield return Coordinates.FourUnitsYZ;
            yield return -Coordinates.FourUnitsZX;
            yield return Coordinates.FourUnitsXY;
            yield return -Coordinates.FourUnitsYZ;
            yield return Coordinates.FourUnitsZX;
            yield return -Coordinates.FourUnitsXY;
        }

        private int GetMaximumsNumberOfShells()
        {
            return Cells.Max(c => c.Coordinates.X) - 1;
        }
    }
}
