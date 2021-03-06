using Battleship.Core.Enums;
using System;

namespace Battleship.Core.Models
{
    /// <summary>
    /// Represent single ship to be placed on board.
    /// </summary>
    public sealed class Ship
    {
        public ClassOfShip ClassOfShip { get; }
        public int Size { get; }
        public Orientation Orientation { get; }

        private Ship(ClassOfShip classOfShip, int size, Orientation orientation)
        {
            ClassOfShip = classOfShip;
            Size = size;
            Orientation = orientation;
        }

        public static Ship MakeShip(ClassOfShip classOfShip, Orientation orientation)
        {
            var ship = classOfShip switch
            {
                ClassOfShip.Carrier => new Ship(ClassOfShip.Carrier, 5, orientation),
                ClassOfShip.Battleship => new Ship(ClassOfShip.Battleship, 4, orientation),
                ClassOfShip.Cruiser => new Ship(ClassOfShip.Cruiser, 3, orientation),
                ClassOfShip.Submarine => new Ship(ClassOfShip.Submarine, 3, orientation),
                ClassOfShip.Destroyer => new Ship(ClassOfShip.Destroyer, 2, orientation),
                _ => throw new ArgumentException(nameof(classOfShip))
            };
            return ship;
        }
    }
}