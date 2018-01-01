using System;
using System.Collections.Generic;
using System.Linq;

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

        }

        private void AssignStartingPlayerCells(IEnumerable<Player> players)
        {
            var coordinates = GetPlayerCoordinates();

            var playerCells = Cells
                .Where(c => coordinates.Any(cd => cd.Equals(c.Coordinates)))
                .Take(players.Count())
                .OrderBy(c => Guid.NewGuid());

            var playerNames = new Queue<string>(players.Select(p => p.Name));

            foreach (var cell in playerCells)
            {
                cell.CellOwner = playerNames.Dequeue();
            }
        }

        private IEnumerable<Coordinates> GetPlayerCoordinates()
        {
            var coordinates = new List<Coordinates>();

            var maxShells = GetMaximumsNumberOfShells();

            int shell;

            for (var n = 0; 4 * n + 1 <= maxShells; n++)
            {
                shell = 4 * n + 1;

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

            var range = Enumerable.Range(1, timesToMove);

            var directions = new List<Coordinates>();

            directions.AddRange(range.SelectMany(i => GetDirections()));

            return directions;
        }

        private IEnumerable<Coordinates> GetDirections()
        {
            yield return Coordinates.UnitYZ;
            yield return -Coordinates.UnitZX;
            yield return Coordinates.UnitXY;
            yield return -Coordinates.UnitYZ;
            yield return Coordinates.UnitZX;
            yield return -Coordinates.UnitXY;
        }

        private int GetMaximumsNumberOfShells()
        {
            return Cells.Max(c => c.Coordinates.X) - 1;
        }
    }
}
