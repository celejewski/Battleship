using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Core.Models
{
    /// <summary>
    /// Board does contain information about board state. 
    /// </summary>
    public sealed class Board
    {
        private readonly Field[,] _fields = new Field[GameConstraint.BoardSize, GameConstraint.BoardSize];

        private readonly Dictionary<ClassOfShip, int> _shipsToAddCount = new()
        {
            {ClassOfShip.Carrier, GameConstraint.CarriersCount},
            {ClassOfShip.Battleship, GameConstraint.BattleshipsCount},
            {ClassOfShip.Cruiser, GameConstraint.CruisersCount},
            {ClassOfShip.Submarine, GameConstraint.SubmarinesCount},
            {ClassOfShip.Destroyer, GameConstraint.DestroyersCount},
        };

        public IReadOnlyDictionary<ClassOfShip, int> ShipsToAddCount => _shipsToAddCount;

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
                    if (CanAddShipOnField(x, y)) return false;
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
                    if (CanAddShipOnField(x, y)) return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Helper method to figure out if field is ship
        /// </summary>
        /// <param name="x">Can be any value</param>
        /// <param name="y">Can be any value</param>
        /// <returns>Returns true when field is ship. In case of x or y out of game board it returns false.</returns>
        private bool FieldIsShip(int x, int y)
        {
            var isIndexOutOfRange = x < 0
                                    || x >= GameConstraint.BoardSize
                                    || y < 0
                                    || y >= GameConstraint.BoardSize;

            if (isIndexOutOfRange) return false;
            return _fields[x, y] == Field.Ship;
        }

        /// <summary>
        /// Helper to check if there is ship on give field or neighbouring fields
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool CanAddShipOnField(int x, int y)
        {
            return FieldIsShip(x - 1, y - 1)
                   || FieldIsShip(x - 1, y + 0)
                   || FieldIsShip(x - 1, y + 1)
                   || FieldIsShip(x, y - 1)
                   || FieldIsShip(x, y + 0)
                   || FieldIsShip(x, y + 1)
                   || FieldIsShip(x + 1, y - 1)
                   || FieldIsShip(x + 1, y + 0)
                   || FieldIsShip(x + 1, y + 1);
        }


        /// <summary>
        /// Add ship to the board on given position.
        /// </summary>
        /// <param name="ship">Ship to add</param>
        /// <param name="position">Position where to add</param>
        public void AddShip(Ship ship, Position position)
        {
            EnsureCanAddShip(ship, position);

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

            _shipsToAddCount[ship.ClassOfShip]--;
        }

        public Field GetField(int x, int y) => _fields[x, y];

        public Field GetField(Position position) => _fields[position.X, position.Y];

        /// <summary>
        /// Reset board state to initial.
        /// </summary>
        public void Reset()
        {
            _shipsToAddCount[ClassOfShip.Carrier] = GameConstraint.CarriersCount;
            _shipsToAddCount[ClassOfShip.Battleship] = GameConstraint.BattleshipsCount;
            _shipsToAddCount[ClassOfShip.Cruiser] = GameConstraint.CruisersCount;
            _shipsToAddCount[ClassOfShip.Submarine] = GameConstraint.SubmarinesCount;
            _shipsToAddCount[ClassOfShip.Destroyer] = GameConstraint.DestroyersCount;

            for (int x = 0; x < GameConstraint.BoardSize; x++)
            {
                for (int y = 0; y < GameConstraint.BoardSize; y++)
                {
                    _fields[x, y] = Field.Water;
                }
            }
        }

        /// <summary>
        /// Fire to given position.
        /// </summary>
        /// <param name="position"></param>
        public void Fire(Position position)
        {
            EnsureAllShipsArePlaced();

            if (_fields[position.X, position.Y] == Field.Water)
            {
                _fields[position.X, position.Y] = Field.Miss;
            }

            if (_fields[position.X, position.Y] == Field.Ship)
            {
                _fields[position.X, position.Y] = Field.Hit;
            }
        }

        /// <summary>
        /// Can be usued only after all ships has been placed.
        /// </summary>
        /// <returns></returns>
        public bool AreAllShipsSunken()
        {
            EnsureAllShipsArePlaced();

            for (int x = 0; x < GameConstraint.BoardSize; x++)
            {
                for (int y = 0; y < GameConstraint.BoardSize; y++)
                {
                    if (_fields[x, y] == Field.Ship) return false;
                }
            }

            return true;
        }

        public bool AreAllShipsPlaced()
        {
            return _shipsToAddCount.Values.All(v => v == 0);
        }

        private void EnsureCanAddShip(Ship ship, Position position)
        {
            if (_shipsToAddCount[ship.ClassOfShip] == 0)
            {
                var message = $"Can not add ship because all available ships of type {ship.ClassOfShip} were already placed.";
                throw new InvalidOperationException(message);
            }

            if (!CanAddShip(ship, position))
            {
                var message = $"Can not add ship to because position {position} is against game rules.";
                throw new InvalidOperationException(message);
            }
        }

        private void EnsureAllShipsArePlaced()
        {
            if (!AreAllShipsPlaced())
            {
                var message = "All ships has to be placed.";
                throw new InvalidOperationException(message);
            }
        }
    }
}