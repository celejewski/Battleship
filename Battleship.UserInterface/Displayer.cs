using Battleship.Core;
using Battleship.Core.Dtos;
using Battleship.Core.Enums;
using Battleship.Core.Models;
using System;

namespace Battleship.UserInterface
{
    public class Displayer
    {
        private const int _rightPlayerConsolePositionOffset = 30;

        public Displayer()
        {
            Console.CursorVisible = false;
        }

        public void Show(GameState gameState, TurnSummary turnSummary)
        {
            Show(gameState);
            ShowTurnSummary(turnSummary);
        }

        public void Show(GameState gameState)
        {
            ShowBoard(0, gameState.LeftBoard);
            ShowBoard(_rightPlayerConsolePositionOffset, gameState.RightBoard);
        }

        private static void ShowTurnSummary(TurnSummary turnSummary)
        {
            Console.Title = turnSummary.MatchStatus.ToString();
            Console.SetCursorPosition(4, 14);
            WritePlayerActionSummary(turnSummary.LeftPlayerActionSummary);
            Console.SetCursorPosition(4 + _rightPlayerConsolePositionOffset, 14);
            WritePlayerActionSummary(turnSummary.RightPlayerActionSummary);

            Console.SetCursorPosition(4, 16);
            Console.WriteLine("Match status: " + turnSummary.MatchStatus);
        }

        private static void WritePlayerActionSummary(PlayerActionSummary playerActionSummary)
        {
            Console.Write(playerActionSummary.Position + ": " + playerActionSummary.Outcome);
        }

        private static void ShowBoard(int offset, IBoard board)
        {
            WriteColumns(offset);
            WriteRows(offset);
            WriteBorder(offset);
            WriteBoard(offset, board);
        }

        private static void WriteBoard(int offset, IBoard board)
        {
            for (int i = 0; i < GameConstraint.BoardSize; i++)
            {
                for (int j = 0; j < GameConstraint.BoardSize; j++)
                {
                    Console.SetCursorPosition(offset + 4 + i, 3 + j);
                    var field = board.GetField(i, j);
                    WriteField(field);
                }
            }
        }

        private static void WriteField(Field field)
        {
            Console.ForegroundColor = field switch
            {
                Field.Miss => ConsoleColor.White,
                _ => ConsoleColor.Black,
            };

            Console.BackgroundColor = field switch
            {
                Field.Ship => ConsoleColor.White,
                Field.Hit => ConsoleColor.DarkGray,
                _ => ConsoleColor.Blue
            };

            var character = field switch
            {
                Field.Hit => 'X',
                Field.Miss => 'o',
                _ => ' '
            };

            Console.Write(character);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void WriteBorder(int offset)
        {
            WriteHorizontalBorder(offset + 4, 2);
            WriteHorizontalBorder(offset + 4, 13);
            WriteVerticalBorder(offset + 3, 3);
            WriteVerticalBorder(offset + 14, 3);
        }

        private static void WriteColumns(int offset)
        {
            Console.SetCursorPosition(offset + 4, 1);
            Console.Write("ABCDEFGHIJ");
        }

        private static void WriteRows(int offset)
        {
            for (int i = 0; i < GameConstraint.BoardSize; i++)
            {
                Console.SetCursorPosition(offset + 0, 3 + i);
                var row = string.Format("{0,2:##}", i + 1);
                Console.Write(row);
            }
        }

        private static void WriteHorizontalBorder(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < GameConstraint.BoardSize; i++)
            {
                Console.Write('-');
            }
        }

        private static void WriteVerticalBorder(int left, int top)
        {
            for (int i = 0; i < GameConstraint.BoardSize; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.Write('|');
            }
        }
    }
}