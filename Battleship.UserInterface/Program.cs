using Battleship.Core;
using System;
using System.Threading;

namespace Battleship.UserInterface
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var displayer = new Displayer();
            var bot = new Bot();
            var boardLeft = bot.MakeStartingBoard();

            var boardRight = bot.MakeStartingBoard();

            var gameState = new GameState
            {
                BoardLeft = boardLeft,
                BoardRight = boardRight
            };
            displayer.Show(gameState);

            Console.ReadLine();
            return;


            DisplayBoard();
            for (int i = 0; i < 7; i++)
            {
                const int delay = 200;
                Console.SetCursorPosition(0, 14);
                Console.WriteLine("   Waiting                              ");
                Thread.Sleep(delay);

                Console.SetCursorPosition(0, 14);
                Console.WriteLine("   Waiting.                             ");
                Thread.Sleep(delay);

                Console.SetCursorPosition(0, 14);
                Console.WriteLine("   Waiting..                            ");
                Thread.Sleep(delay);

                Console.SetCursorPosition(0, 14);
                Console.WriteLine("   Waiting...                           ");
                Thread.Sleep(delay);
            }


            Console.SetCursorPosition(0, 14);
            Console.WriteLine("   J1                                      ");
            Thread.Sleep(500);

            Console.SetCursorPosition(0, 0);
            DisplayBoard_2();

            for (int i = 0; i < 3; i++)
            {
                var millisecondsTimeout = 200;
                Console.SetCursorPosition(36, 3);
                Console.Write('o');
                Thread.Sleep(millisecondsTimeout);
                Console.SetCursorPosition(36, 3);
                Console.Write(' ');
                Thread.Sleep(millisecondsTimeout);
            }


            Console.SetCursorPosition(36, 3);
            Console.Write('o');
            Console.ReadLine();
        }

        private static void DisplayBoard()
        {
            Console.WriteLine("                                      ");
            Console.WriteLine("   ABCDEFGHIJ              ABCDEFGHIJ ");
            Console.WriteLine("   ----------              ---------- ");
            Console.WriteLine(" 1|###       |           1|###       |");
            Console.WriteLine(" 2|          |           2|          |");
            Console.WriteLine(" 3|   #     o|           3|   #     o|");
            Console.WriteLine(" 4|   Xo    #|           4|   Xo    #|");
            Console.WriteLine(" 5|  oXo    #|           5|  oXo    #|");
            Console.WriteLine(" 6|  oo   #o.|           6|  oo   #o.|");
            Console.WriteLine(" 7|       #  |           7|       #  |");
            Console.WriteLine(" 8|    ###   |           8|    ###   |");
            Console.WriteLine(" 9|          |           9|          |");
            Console.WriteLine("10|o         |           0|o         |");
            Console.WriteLine("   ----------              ---------- ");
            Console.WriteLine("   Waiting...                         ");
            Console.WriteLine("                                      ");
        }

        private static void DisplayBoard_2()
        {
            Console.WriteLine("                                      ");
            Console.WriteLine("   ABCDEFGHIJ              ABCDEFGHIJ ");
            Console.WriteLine("   ----------              ---------- ");
            Console.WriteLine(" 1|###       |           1|###      o|");
            Console.WriteLine(" 2|          |           2|          |");
            Console.WriteLine(" 3|   #     o|           3|   #     o|");
            Console.WriteLine(" 4|   Xo    #|           4|   Xo    #|");
            Console.WriteLine(" 5|  oXo    #|           5|  oXo    #|");
            Console.WriteLine(" 6|  oo   #o.|           6|  oo   #o.|");
            Console.WriteLine(" 7|       #  |           7|       #  |");
            Console.WriteLine(" 8|    ###   |           8|    ###   |");
            Console.WriteLine(" 9|          |           9|          |");
            Console.WriteLine("10|o         |           0|o         |");
            Console.WriteLine("   ----------              ---------- ");
            Console.WriteLine("   J1                      Miss       ");
            Console.WriteLine("                                      ");
        }
    }
}