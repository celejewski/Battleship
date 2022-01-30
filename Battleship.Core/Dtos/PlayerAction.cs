using Battleship.Core.Enums;
using Battleship.Core.Models;

namespace Battleship.Core.Dtos
{
    public record PlayerAction(Player Player, Position Position, Outcome Outcome);
}