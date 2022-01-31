using Battleship.Core.Dtos;
using Battleship.Core.Enums;
using System;

namespace Battleship.Core.Models
{
    public sealed class BotMatch
    {
        private readonly Bot _leftBot;
        private readonly Bot _rightBot;
        private readonly Board _leftBoard;
        private readonly Board _rightBoard;
        public GameState GameState { get; }

        public BotMatch()
        {
            _leftBot = new();
            _rightBot = new();
            _leftBoard = _leftBot.MakeStartingBoard();
            _rightBoard = _rightBot.MakeStartingBoard();
            GameState = new GameState(_leftBoard, _rightBoard);
        }

        /// <summary>
        /// Performs game turn.
        /// </summary>
        /// <returns>Summary of what happened during this turn</returns>
        public TurnSummary Turn()
        {
            if (GetMatchStatus() != MatchStatus.Running)
            {
                var message = "Game has already finished.";
                throw new InvalidOperationException(message);
            }

            var leftPlayerAction = PlayerAction(Player.Left);
            var rightPlayerAction = PlayerAction(Player.Right);
            var matchStatus = GetMatchStatus();

            var turnSummary = new TurnSummary(leftPlayerAction, rightPlayerAction, matchStatus);
            return turnSummary;
        }


        public MatchStatus GetMatchStatus()
        {
            var leftKilled = _leftBoard.AreAllShipsSunk();
            var rightKilled = _rightBoard.AreAllShipsSunk();

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

            var board = player == Player.Left ? _rightBoard : _leftBoard;
            board.Fire(position);
            var outcome = board.GetField(position) == Field.Miss
                ? Outcome.Miss
                : Outcome.Hit;

            var playerAction = new PlayerActionSummary(player, position, outcome);
            return playerAction;
        }
    }
}