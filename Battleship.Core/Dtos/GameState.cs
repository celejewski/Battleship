using Battleship.Core.Models;

namespace Battleship.Core.Dtos
{
    public sealed record GameState(Board LeftBoard, Board RightBoard);
}