using System;

namespace Gone
{
    public class Cell : ICell
    {
        public Guid Id { get; set; }

        public int Resources { get; set; }

        public Coordinates Coordinates { get; set; }

        public Player CellOwner { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Cell;
            if (other == null)
                return false;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
