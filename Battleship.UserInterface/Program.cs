using System;
using System.Threading;

namespace Battleship.UserInterface
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var displayer = new Displayer();
            var boardLeft = new Board();
            boardLeft.Fields[5, 5] = Field.Miss;
            boardLeft.Fields[5, 6] = Field.Miss;
            boardLeft.Fields[5, 7] = Field.Miss;
            boardLeft.Fields[6, 5] = Field.Miss;
            boardLeft.Fields[7, 5] = Field.Miss;
            boardLeft.Fields[7, 7] = Field.Hit;
            boardLeft.Fields[7, 8] = Field.Hit;
            boardLeft.Fields[0, 0] = Field.Ship;
            boardLeft.Fields[0, 1] = Field.Ship;
            boardLeft.Fields[0, 2] = Field.Ship;
            boardLeft.Fields[0, 3] = Field.Ship;
            boardLeft.Fields[0, 4] = Field.Ship;

            var boardRight = boardLeft;

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