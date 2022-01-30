using Battleship.Core.Enums;

namespace Battleship.Core.Dtos
{
    public record TurnSummary(
        PlayerActionSummary LeftPlayerActionSummary,
        PlayerActionSummary RightPlayerActionSummary,
        MatchStatus MatchStatus);
}