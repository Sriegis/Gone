using System.Collections.Generic;

namespace Gone
{
    public class PlayerCoordinateNavigator
    {
        public Coordinates StartingPoint { get; set; }
        public IEnumerable<Coordinates> Directions { get; set; }
        public int TimesToMove { get; set; }
    }
}
