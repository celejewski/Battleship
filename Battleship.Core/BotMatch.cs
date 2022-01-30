namespace Battleship.Core
{
    public class BotMatch
    {
        private readonly Bot _leftBot;
        private readonly Bot _rightBot;
        public GameState GameState { get; }

        public BotMatch()
        {
            _leftBot = new();
            _rightBot = new();
            GameState = new GameState
            {
                BoardLeft = _leftBot.MakeStartingBoard(),
                BoardRight = _rightBot.MakeStartingBoard(),
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
            var position = player == Player.Left
                ? _leftBot.GetPositionToFireAt()
                : _rightBot.GetPositionToFireAt();

            var board = player == Player.Left ? GameState.BoardRight : GameState.BoardLeft;
            board.Fire(position);
            var outcome = board.GetField(position) == Field.Miss
                ? Outcome.Miss
                : Outcome.Hit;

            var playerAction = new PlayerAction
            {
                Player = player,
                Position = position,
                Outcome = outcome
            };
            return playerAction;
        }
    }
}