﻿namespace Battleship.UserInterface
{
    public class GameState
    {
        public Board BoardLeft { get; set; }
        public Board BoardRight { get; set; }
        public Player ActivePlayer { get; set; }
    }
}