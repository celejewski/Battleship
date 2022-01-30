using System;

namespace Battleship.Core
{
    public class Board
    {
        private readonly Field[,] _fields = new Field[10, 10];

        /// <summary>
        /// Checks if ship can be added to board.
        /// </summary>
        /// <param name="ship">Ship to be checked</param>
        /// <param name="position">Left top corner of ship</param>
        /// <returns></returns>
        public bool CanAddShip(Ship ship, Position position)
        {
            if (ship.Orientation == Orientation.Horizontal)
            {
                if (position.X + ship.Size >= GameConstraint.BoardSize) return false;

                for (int i = 0; i < ship.Size; i++)
                {
                    int x = position.X + i;
                    int y = position.Y;
                    if (_fields[x, y] == Field.Ship) return false;
                }

                return true;
            }
            else
            {
                if (position.Y + ship.Size >= GameConstraint.BoardSize) return false;

                for (int i = 0; i < ship.Size; i++)
                {
                    int x = position.X;
                    int y = position.Y + i;
                    if (_fields[x, y] == Field.Ship) return false;
                }

                return true;
            }
        }

        public void AddShip(Ship ship, Position position)
        {
            if (!CanAddShip(ship, position)) throw new InvalidOperationException();

            for (int i = 0; i < ship.Size; i++)
            {
                if (ship.Orientation == Orientation.Horizontal)
                {
                    _fields[position.X + i, position.Y] = Field.Ship;
                }
                else
                {
                    _fields[position.X, position.Y + i] = Field.Ship;
                }
            }
        }

        public Field GetField(int x, int y) => _fields[x, y];
    }
}