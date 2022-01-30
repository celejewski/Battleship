﻿using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Core.Models
{
    public sealed class Board
    {
        private readonly Field[,] _fields = new Field[10, 10];

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

        private bool FieldIsShip(int x, int y)
        {
            var isIndexOutOfRange = x < 0
                                    || x >= GameConstraint.BoardSize
                                    || y < 0
                                    || y >= GameConstraint.BoardSize;

            if (isIndexOutOfRange) return false;
            return _fields[x, y] == Field.Ship;
        }

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

        private bool AreAllShipsPlaced()
        {
            return _shipsToAddCount.Values.All(v => v == 0);
        }

        private void EnsureCanAddShip(Ship ship, Position position)
        {
            if (_shipsToAddCount[ship.ClassOfShip] == 0) throw new InvalidOperationException();
            if (!CanAddShip(ship, position)) throw new InvalidOperationException();
        }

        private void EnsureAllShipsArePlaced()
        {
            if (!AreAllShipsPlaced()) throw new InvalidOperationException();
        }
    }
}