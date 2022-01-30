﻿using Battleship.Core;
using Battleship.Core.Dtos;
using Battleship.Core.Enums;
using Battleship.Core.Models;
using System;

namespace Battleship.UserInterface
{
    public class Displayer
    {
        public Displayer()
        {
            Console.CursorVisible = false;
        }

        public void Show(GameState gameState)
        {
            ShowBoard(0, gameState.LeftBoard);
            ShowBoard(30, gameState.RightBoard);
        }

        private static void ShowBoard(int offset, Board board)
        {
            WriteColumns(offset);
            WriteRows(offset);
            WriteBorder(offset);
            WriteBoard(offset, board);
        }

        private static void WriteBoard(int offset, Board board)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
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
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(offset + 0, 3 + i);
                var row = string.Format("{0,2:##}", i + 1);
                Console.Write(row);
            }
        }

        private static void WriteHorizontalBorder(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < 10; i++)
            {
                Console.Write('-');
            }
        }

        private static void WriteVerticalBorder(int left, int top)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.Write('|');
            }
        }
    }
}