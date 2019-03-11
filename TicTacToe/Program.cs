using System;

namespace TicTacToe
{
    class Program
    {

        static void Main()
        {
            var game = new TicTacToeGame();
            game.Cols = 3;
            game.Rows = 3;
            game.NewGame();

            var winner = TicTacToeGame.TicTacToePlayer.None;

            while (winner == TicTacToeGame.TicTacToePlayer.None)
            {
                Console.SetCursorPosition(0, 0);
                game.PrintBoard();

                game.SetCursorLocation();
                var key = Console.ReadKey(false);

                if (key.Key == ConsoleKey.UpArrow) game.MoveUp();
                if (key.Key == ConsoleKey.DownArrow) game.MoveDown();
                if (key.Key == ConsoleKey.RightArrow) game.MoveRight();
                if (key.Key == ConsoleKey.LeftArrow) game.MoveLeft();

                if (key.Key == ConsoleKey.Spacebar)
                {
                    game.PlayLocation();
                    winner = game.CheckForWiner();
                }
                if (key.Key == ConsoleKey.R) game.NewGame();
                if (key.Key == ConsoleKey.Escape) return;

            }

            Console.SetCursorPosition(0, 0);
            game.PrintBoard();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Winner! {winner} ({TicTacToeGame.GetPlayerChar(winner)})");

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
    }
}
