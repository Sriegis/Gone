using System;
using System.Collections.Generic;
using System.Linq;

namespace Gone
{
    public class CellGenerator
    {
        private readonly Queue<Coordinates> _coordinateCache = new Queue<Coordinates>();
        private int _currentShell = 0;
        private int _playerCount;

        public CellGenerator(int playerCount)
        {
            _playerCount = playerCount;
        }

        public Cell[] Generate()
        {
            var numberOfCells = GetCellCount(_playerCount);

            var cells = new Cell[numberOfCells];

            for (int i = 0; i < numberOfCells; i++)
            {
                cells[i] = new Cell
                {
                    Id = Guid.NewGuid(),
                    Coordinates = GetNextCoordinates(),
                    Resources = 0,
                    CellOwner = "None"
                };
            }

            _currentShell = 0;

            return cells.ToArray();
        }

        private int GetCellCount(int playerCount)
        {
            var playerCeiling = (int)Math.Ceiling((playerCount - 1)/6d);
            return 48 * playerCeiling * playerCeiling + 60 * playerCeiling + 19;
        }

        private Coordinates GetNextCoordinates()
        {
            if (!_coordinateCache.Any())
                RegenerateCoordinateCache(_currentShell++);

            return _coordinateCache.Dequeue();
        }

        private void RegenerateCoordinateCache(int shellNumber)
        {
            for (int i = -shellNumber; i <= shellNumber; i++)
            {
                for (int j = -shellNumber; j <= shellNumber; j++)
                {
                    for (int k = -shellNumber; k <= shellNumber; k++)
                    {
                        if (Coordinates.GetDistance(i, 0, j, 0, k, 0) == shellNumber &&
                            i + j + k == 0 &&
                            IsAnyAbsoluteEqualToShell(shellNumber, i, j, k))
                        {
                            _coordinateCache.Enqueue(new Coordinates(i, j, k));
                        }
                    }
                }
            } 
        }

        private static bool IsAnyAbsoluteEqualToShell(int shellNumber, int i, int j, int k)
        {
            return Math.Abs(i) == shellNumber ||
                   Math.Abs(j) == shellNumber ||
                   Math.Abs(k) == shellNumber;
        }
    }
}