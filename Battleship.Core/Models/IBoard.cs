using Battleship.Core.Enums;

namespace Battleship.Core.Models
{
    public interface IBoard
    {
        Field GetField(int x, int y);
        Field GetField(Position position);
    }
}