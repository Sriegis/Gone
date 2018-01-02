using System;

namespace Gone
{
    public class Coordinates
    {
        public static Coordinates Origin => new Coordinates(0, 0, 0);
        public static Coordinates FourUnitsYZ => new Coordinates(0, 4, -4);
        public static Coordinates FourUnitsZX => new Coordinates(-4, 0, 4);
        public static Coordinates FourUnitsXY => new Coordinates(4, -4, 0);

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Coordinates(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Coordinates other))
                return false;

            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override int GetHashCode()
        {
            return (X + 10 * Y + 100 * Z).GetHashCode();
        }

        public static double GetDistance(Coordinates c1, Coordinates c2) =>
            0.5d * (Math.Abs(c1.X - c2.X) + Math.Abs(c1.Y - c2.Y) + Math.Abs(c1.Z - c2.Z));

        public static double GetDistance(int x1, int x2, int y1, int y2, int z1, int z2) =>
            0.5d * (Math.Abs(x1 - x2) + Math.Abs(y1 - y2) + Math.Abs(z1 - z2));

        public static Coordinates operator +(Coordinates c1, Coordinates c2) =>
            new Coordinates(c1.X + c2.X, c1.Y + c2.Y, c1.Z + c2.Z);

        public static Coordinates operator -(Coordinates c1, Coordinates c2) =>
            new Coordinates(c1.X - c2.X, c1.Y - c2.Y, c1.Z - c2.Z);

        public static Coordinates operator -(Coordinates c1) =>
            new Coordinates(-c1.X, -c1.Y, -c1.Z);
    }
}