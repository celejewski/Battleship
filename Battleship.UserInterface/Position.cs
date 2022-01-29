namespace Battleship.UserInterface
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
    }
}