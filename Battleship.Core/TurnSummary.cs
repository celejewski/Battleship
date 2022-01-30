namespace Battleship.Core
{
    public class TurnSummary
    {
        public PlayerAction LeftPlayerAction { get; set; }
        public PlayerAction RightPlayerAction { get; set; }
        public MatchStatus MatchStatus { get; set; }
    }
}