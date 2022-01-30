using Battleship.Core.Models;

namespace Battleship.Core.Dtos
{
    public sealed record GameState(IBoard LeftBoard, IBoard RightBoard);
}