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
                    if (IsFieldAShipOrTouchesTheShip(x, y)) return false;
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
                    if (IsFieldAShipOrTouchesTheShip(x, y)) return false;
                }

                return true;
            }
        }

        private bool IsFieldAShipOrTouchesTheShip(int x, int y)
        {
            bool IsFieldAShip(int x, int y)
            {
                var isIndexOutOfRange = x < 0
                                        || x >= GameConstraint.BoardSize
                                        || y < 0
                                        || y >= GameConstraint.BoardSize;

                if (isIndexOutOfRange) return false;
                return _fields[x, y] == Field.Ship;
            }

            return IsFieldAShip(x - 1, y - 1)
                   || IsFieldAShip(x - 1, y + 0)
                   || IsFieldAShip(x - 1, y + 1)
                   || IsFieldAShip(x, y - 1)
                   || IsFieldAShip(x, y + 0)
                   || IsFieldAShip(x, y + 1)
                   || IsFieldAShip(x + 1, y - 1)
                   || IsFieldAShip(x + 1, y + 0)
                   || IsFieldAShip(x + 1, y + 1);
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