using System;
using System.Collections.Generic;
using System.Linq;

namespace Gone
{
    public class CellGenerator
    {
        private readonly Queue<Coordinates> _coordinateCache = new Queue<Coordinates>();
        private int _currentShell;
        private readonly int _playerCount;

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
            var shellCount = GetShellCount(playerCount);
            var cellCount = 3 * shellCount * (shellCount - 1) + 1;
            return cellCount;
        }

        private int GetShellCount(int playerCount)
        {
            int n = 0;
            int k = 1;

            for (; n < playerCount; k++)
            {
                n = 1 + GetSum(k);
            }

            return 4 * k - 5;
        }

        private int GetSum(int k)
        {
            var sum = 0;
            for (var i = 1; i <= k; i++)
            {
                sum += 6 * (i - 1);
            }
            return sum;
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