namespace Battleship.Core
{
    public class BotMatch
    {
        private static readonly Bot _bot = new();
        public GameState GameState { get; }

        public BotMatch()
        {
            GameState = new GameState
            {
                BoardLeft = _bot.MakeStartingBoard(),
                BoardRight = _bot.MakeStartingBoard(),
                ActivePlayer = Player.Left
            };
        }

        public TurnSummary Turn()
        {
            var turnSummary = new TurnSummary
            {
                LeftPlayerAction = PlayerAction(Player.Left),
                RightPlayerAction = PlayerAction(Player.Right),
                MatchStatus = GetMatchStatus()
            };
            return turnSummary;
        }

        private MatchStatus GetMatchStatus()
        {
            var leftKilled = GameState.BoardLeft.AreAllShipsSunken();
            var rightKilled = GameState.BoardRight.AreAllShipsSunken();

            if (leftKilled && rightKilled) return MatchStatus.Draw;
            if (leftKilled) return MatchStatus.RightWon;
            if (rightKilled) return MatchStatus.LeftWon;

            return MatchStatus.Running;
        }

        private PlayerAction PlayerAction(Player player)
        {
            var position = _bot.GetPositionToFireAt();

            var board = player == Player.Left ? GameState.BoardRight : GameState.BoardLeft;
            board.Fire(position);
            var outcome = board.GetField(position) == Field.Miss
                ? Outcome.Miss
                : Outcome.Hit;

            var logAction = new PlayerAction
            {
                Player = player,
                Position = position,
                Outcome = outcome
            };
            return logAction;
        }
    }
}