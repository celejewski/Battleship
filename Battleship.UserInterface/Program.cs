using Battleship.Core.Dtos;
using Battleship.Core.Enums;
using Battleship.Core.Models;
using System;
using System.Threading;

namespace Battleship.UserInterface
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var displayer = new Displayer();
            var botMatch = new BotMatch();

            TurnSummary turnSummary;
            displayer.Show(botMatch.GameState);
            while (true)
            {
                turnSummary = botMatch.Turn();
                if (turnSummary.MatchStatus != MatchStatus.Running) break;
                displayer.Show(botMatch.GameState, turnSummary);
                Thread.Sleep(20);
            }

            displayer.Show(botMatch.GameState, turnSummary);
            Console.ReadLine();
        }
    }
}