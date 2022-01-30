using Battleship.Core.Enums;
using Battleship.Core.Models;

namespace Battleship.Core.Dtos
{
    public record PlayerActionSummary(Player Player, Position Position, Outcome Outcome);
}