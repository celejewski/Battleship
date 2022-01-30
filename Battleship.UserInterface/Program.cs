using Battleship.Core.Dtos;
using Battleship.Core.Enums;
using Battleship.Core.Models;
using System;
using System.Threading.Tasks;

namespace Battleship.UserInterface
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var displayer = new Displayer();
            while (true)
            {
                var delayFunction = ChooseDelayFunction();
                Console.Clear();
                await StartMatch(displayer, delayFunction);

                Console.SetCursorPosition(4, 18);
                Console.WriteLine("Game ended, press enter to start again.");
                Console.ReadLine();
            }
        }

        private static async Task StartMatch(Displayer displayer, Func<Task> delayFunction)
        {
            var botMatch = new BotMatch();
            TurnSummary turnSummary;
            displayer.Show(botMatch.GameState);
            while (true)
            {
                turnSummary = botMatch.Turn();
                if (turnSummary.MatchStatus != MatchStatus.Running) break;
                displayer.Show(botMatch.GameState, turnSummary);
                await delayFunction();
            }

            displayer.Show(botMatch.GameState, turnSummary);
        }

        private static Func<Task> ChooseDelayFunction()
        {
            Console.Title = "Choose game speed";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose game speed:");
                Console.WriteLine("1. Fast (20ms between turns)");
                Console.WriteLine("2. Medium (200ms between turns)");
                Console.WriteLine("3. Slow (1000 ms between turns)");
                Console.WriteLine("4. Manual - press enter to advance to next turn");
                var line = Console.ReadLine();

                if (!int.TryParse(line, out int value)) continue;
                if (value < 1 || value > 4) continue;

                var delayFunctions = new Func<Task>[]
                {
                    () => Task.Delay(20),
                    () => Task.Delay(200),
                    () => Task.Delay(1000),
                    () =>
                    {
                        Console.ReadLine();
                        return Task.CompletedTask;
                    },
                };
                return delayFunctions[value - 1];
            }
        }
    }
}