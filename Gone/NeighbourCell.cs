using System;

namespace Gone
{
    public class NeighbourCell : ICell
    {
        public int Resources { get; set; }
        public Guid Id { get; set; }
        public Coordinates Coordinates { get; set; }
        public string CellOwner { get; set; }
        public CellOwner Owner { get; set; }
    }
}
