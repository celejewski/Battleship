using Battleship.Core.Dtos;
using Battleship.Core.Enums;

namespace Battleship.Core.Models
{
    public sealed class BotMatch
    {
        private readonly Bot _leftBot;
        private readonly Bot _rightBot;
        public GameState GameState { get; }

        public BotMatch()
        {
            _leftBot = new();
            _rightBot = new();
            var leftBoard = _leftBot.MakeStartingBoard();
            var rightBoard = _rightBot.MakeStartingBoard();
            GameState = new GameState(leftBoard, rightBoard);
        }

        /// <summary>
        /// Performs game turn.
        /// </summary>
        /// <returns>Summary of what happened during this turn</returns>
        public TurnSummary Turn()
        {
            var leftPlayerAction = PlayerAction(Player.Left);
            var rightPlayerAction = PlayerAction(Player.Right);
            var matchStatus = GetMatchStatus();

            var turnSummary = new TurnSummary(leftPlayerAction, rightPlayerAction, matchStatus);
            return turnSummary;
        }


        private MatchStatus GetMatchStatus()
        {
            var leftKilled = GameState.LeftBoard.AreAllShipsSunken();
            var rightKilled = GameState.RightBoard.AreAllShipsSunken();

            if (leftKilled && rightKilled) return MatchStatus.Draw;
            if (leftKilled) return MatchStatus.RightWon;
            if (rightKilled) return MatchStatus.LeftWon;

            return MatchStatus.Running;
        }

        /// <summary>
        /// Get position to fire at and create player summary.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private PlayerActionSummary PlayerAction(Player player)
        {
            var position = player == Player.Left
                ? _leftBot.GetPositionToFireAt()
                : _rightBot.GetPositionToFireAt();

            var board = player == Player.Left ? GameState.RightBoard : GameState.LeftBoard;
            board.Fire(position);
            var outcome = board.GetField(position) == Field.Miss
                ? Outcome.Miss
                : Outcome.Hit;

            var playerAction = new PlayerActionSummary(player, position, outcome);
            return playerAction;
        }
    }
}