namespace Battleship.Core
{
    public class LogAction
    {
        public GameState GameStateBeforeAction { get; set; }
        public Player Player { get; set; }
        public Position Position { get; set; }
        public Outcome Outcome { get; set; }
        public GameState GameStateAfterAction { get; set; }
    }
}