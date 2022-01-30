using System;

namespace Battleship.Core
{
    public class Bot
    {
        private static readonly Random _random = new();

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

        public Position GetPositionToFireAt()
        {
            var x = _random.Next(GameConstraint.BoardSize);
            var y = _random.Next(GameConstraint.BoardSize);
            var position = new Position(x, y);
            return position;
        }

        private void PlaceShips(Board board, ClassOfShip classOfShip)
        {
            while (board.ShipsToAddCount[classOfShip] > 0)
            {
                PlaceShip(board, classOfShip);
            }
        }

        private static void PlaceShip(Board board, ClassOfShip classOfShip)
        {
            var retriesLimit = 100;
            for (int j = 0; j < retriesLimit; j++)
            {
                var orientation = _random.Next(2) == 1 ? Orientation.Horizontal : Orientation.Vertical;
                var ship = Ship.MakeShip(classOfShip, orientation);
                var x = _random.Next(0, GameConstraint.BoardSize);
                var y = _random.Next(0, GameConstraint.BoardSize);
                var position = new Position(x, y);

                if (!board.CanAddShip(ship, position)) continue;
                board.AddShip(ship, position);
                return;
            }

            throw new InvalidOperationException();
        }
    }
}