using System;

namespace Battleship.Core.Models
{
    /// <summary>
    /// Position inside board.
    /// </summary>
    public sealed class Position
    {
        private static readonly Random _random = new();

        public int X { get; set; }

        public int Y { get; set; }

        public Position(int x, int y)
        {
            var message = $"Value has to be between 0 and {GameConstraint.BoardSize}";
            if (x < 0 || x >= GameConstraint.BoardSize) throw new ArgumentOutOfRangeException(nameof(x), x, message);
            if (y < 0 || y >= GameConstraint.BoardSize) throw new ArgumentOutOfRangeException(nameof(y), y, message);

            Y = y;
            X = x;
        }

        public static Position MakeRandomPosition()
        {
            var x = _random.Next(GameConstraint.BoardSize);
            var y = _random.Next(GameConstraint.BoardSize);
            var position = new Position(x, y);
            return position;
        }

        public override string ToString()
        {
            const string columns = "ABCDEFGHIJ";
            return columns[X] + Y.ToString();
        }

        private bool Equals(Position other)
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