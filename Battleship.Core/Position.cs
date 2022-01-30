using System;

namespace Battleship.Core
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            const string columns = "ABCDEFGHIJ";
            return columns[X] + Y.ToString();
        }

        public Position(int x, int y)
        {
            var message = $"Value has to be between 0 and {GameConstraint.BoardSize}";
            if (x < 0 || x > 9) throw new ArgumentOutOfRangeException(nameof(x), x, message);
            if (y < 0 || y > 9) throw new ArgumentOutOfRangeException(nameof(y), y, message);

            Y = y;
            X = x;
        }

        protected bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}