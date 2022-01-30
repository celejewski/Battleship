using Battleship.Core.Enums;
using System;
using System.Collections.Generic;

namespace Battleship.Core.Models
{
    public sealed class Bot
    {
        private static readonly Random _random = new();
        private readonly HashSet<Position> _positions = new();

        /// <summary>
        /// Makes starting board by randomly placing all the ships.
        /// </summary>
        /// <returns></returns>
        public Board MakeStartingBoard()
        {
            var board = new Board();
            while (true)
            {
                try
                {
                    PlaceShips(board, ClassOfShip.Carrier);
                    PlaceShips(board, ClassOfShip.Battleship);
                    PlaceShips(board, ClassOfShip.Cruiser);
                    PlaceShips(board, ClassOfShip.Submarine);
                    PlaceShips(board, ClassOfShip.Destroyer);
                    return board;
                }
                catch (Exception)
                {
                    board.Reset();
                }
            }
        }


        /// <summary>
        /// Returns random position to fire at. Does shoot each position at most once.
        /// </summary>
        /// <returns></returns>
        public Position GetPositionToFireAt()
        {
            if (_positions.Count == GameConstraint.BoardSize * GameConstraint.BoardSize) throw new InvalidOperationException();

            Position position;
            do
            {
                position = Position.MakeRandomPosition();
            } while (!_positions.Add(position));

            return position;
        }

        /// <summary>
        /// Helper to place all ships of given category.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="classOfShip"></param>
        private void PlaceShips(Board board, ClassOfShip classOfShip)
        {
            while (board.ShipsToAddCount[classOfShip] > 0)
            {
                PlaceShip(board, classOfShip);
            }
        }

        /// <summary>
        /// Tries to place ship of given ship. If retries fails multiple times it will throw exception beacuse it is very likely that board is in state in which it is impossible to place ship.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="classOfShip"></param>
        private static void PlaceShip(Board board, ClassOfShip classOfShip)
        {
            var retriesLimit = 100;
            for (int j = 0; j < retriesLimit; j++)
            {
                var orientation = _random.Next(2) == 1 ? Orientation.Horizontal : Orientation.Vertical;
                var ship = Ship.MakeShip(classOfShip, orientation);
                var position = Position.MakeRandomPosition();

                if (!board.CanAddShip(ship, position)) continue;
                board.AddShip(ship, position);
                return;
            }

            var message = "Placing ship failed, it is very likely that board is in state where ship can not be added";
            throw new InvalidOperationException(message);
        }
    }
}