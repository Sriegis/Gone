using System;

namespace Gone
{
    public interface ICell
    {
        Coordinates Coordinates { get; set; }
        Guid Id { get; set; }
        int Resources { get; set; }
        string CellOwner { get; set; }
    }
}
