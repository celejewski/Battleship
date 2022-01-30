namespace Battleship.Core
{
    public class Ship
    {
        public int Size { get; }
        public Orientation Orientation { get; }

        private Ship(int size, Orientation orientation)
        {
            Size = size;
            Orientation = orientation;
        }

        public static Ship MakeShip(ClassOfShip classOfShip, Orientation orientation)
        {
            var ship = classOfShip switch
            {
                ClassOfShip.Carrier => new Ship(5, orientation),
                ClassOfShip.Battleship => new Ship(4, orientation),
                ClassOfShip.Cruiser => new Ship(3, orientation),
                ClassOfShip.Submarine => new Ship(3, orientation),
                ClassOfShip.Destroyer => new Ship(2, orientation),
            };
            return ship;
        }
    }
}