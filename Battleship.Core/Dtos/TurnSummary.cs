using Battleship.Core.Enums;

namespace Battleship.Core.Dtos
{
    public record TurnSummary(PlayerAction LeftPlayerAction, PlayerAction RightPlayerAction, MatchStatus MatchStatus);
}