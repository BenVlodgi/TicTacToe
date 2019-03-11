using System;
using System.Collections.Generic;

namespace TicTacToe
{

    public class TicTacToeGame
    {
        protected enum SearchDirection
        {
            Forward = 1,
            ForwardDown = 2,
            Down = 3,
            BackwardDown = 4
        }

        public enum TicTacToePlayer
        {
            None = 0,
            Tic = 1,
            Tac = 2
        }

        protected TicTacToePlayer[,] Board;
        protected TicTacToePlayer CurrentPlayer = TicTacToePlayer.Tic;

        public int Rows = 3;
        public int Cols = 3;

        public int cursorRow = 0;
        public int cursorCol = 0;
        
        public int ChainForWin = 3;
        

        public TicTacToeGame()
        {
            NewGame();
            CurrentPlayer = TicTacToePlayer.Tic;
        }
        
        public void NewGame()
        {
            Board = new TicTacToePlayer[Rows, Cols];
        }

        public void PrintBoard()
        {
            Console.WriteLine($"CurrentPlayer: {CurrentPlayer} ({GetPlayerChar(CurrentPlayer)})");
            Console.WriteLine();


            bool firstRow = true;
            bool firstCol = true;
            for (int row = 0; row < Rows; row++)
            {
                //Console.Write(BoardLeftPad);

                if (!firstRow) Console.WriteLine(new string('-', (Cols * 2) - 1));
                else firstRow = false;

                firstCol = true;
                for (int col = 0; col < Cols; col++)
                {
                    if (!firstCol) Console.Write('|');
                    else firstCol = false;

                    var player = Board[row, col];
                    Console.Write(GetPlayerChar(player));
                }
                Console.WriteLine();
            }
        }

        public void MoveUp()
        {
            cursorRow = Math.Max(0, cursorRow - 1);
        }

        public void MoveDown()
        {
            cursorRow = Math.Min(Rows - 1, cursorRow + 1);
        }

        public void MoveLeft()
        {
            cursorCol = Math.Max(0, cursorCol - 1);
        }

        public void MoveRight()
        {
            cursorCol = Math.Min(Cols - 1, cursorCol + 1);
        }

        public void PlayLocation()
        {
            if (cursorRow >= Rows || cursorCol >= Cols)
                return;

            if (Board[cursorRow, cursorCol] == TicTacToePlayer.None)
            {
                Board[cursorRow, cursorCol] = CurrentPlayer;
                NextPlayer();
            }
        }

        public static char GetPlayerChar(TicTacToePlayer player)
        {
            return player == TicTacToePlayer.None ? ' '
                 : player == TicTacToePlayer.Tic ? 'X'
                 : player == TicTacToePlayer.Tac ? 'O'
                 : '?'
                 ;
        }

        public void NextPlayer()
        {
            CurrentPlayer = CurrentPlayer == TicTacToePlayer.Tic
                ? TicTacToePlayer.Tac
                : TicTacToePlayer.Tic;
        }

        public void SetCursorLocation()
        {
            int left = cursorCol * 2;
            int right = cursorRow * 2 + 2;
            Console.SetCursorPosition(left, right);
        }

        public TicTacToePlayer CheckForWiner()
        {
            for (int row = 0; row < Rows; row++) for (int col = 0; col < Cols; col++)
                {
                    TicTacToePlayer winner;

                    // Check Forward
                    winner = CheckChainForWinner(GetChain(row, col, SearchDirection.Forward));
                    if (winner != TicTacToePlayer.None)
                        return winner;

                    // Check Forward Down
                    winner = CheckChainForWinner(GetChain(row, col, SearchDirection.ForwardDown));
                    if (winner != TicTacToePlayer.None)
                        return winner;

                    // Check Down
                    winner = CheckChainForWinner(GetChain(row, col, SearchDirection.Down));
                    if (winner != TicTacToePlayer.None)
                        return winner;

                    // Check Back Down
                    winner = CheckChainForWinner(GetChain(row, col, SearchDirection.BackwardDown));
                    if (winner != TicTacToePlayer.None)
                        return winner;

                }

            return TicTacToePlayer.None;
        }

        protected List<TicTacToePlayer> GetChain(int row, int col, SearchDirection direction)
        {
            var chain = new List<TicTacToePlayer>();
            int x = row;
            int y = col;

            while (x >= 0 && x < Rows
                && y >= 0 && y < Cols
                && chain.Count < ChainForWin
                && Board[x, y] != TicTacToePlayer.None)
            {
                chain.Add(Board[x, y]);
                switch (direction)
                {
                    case SearchDirection.Forward: y++; break;
                    case SearchDirection.ForwardDown: y++; x++; break;
                    case SearchDirection.Down: x++; break;
                    case SearchDirection.BackwardDown: y--; x++; break;
                }
            }
            return chain;
        }

        protected TicTacToePlayer CheckChainForWinner(List<TicTacToePlayer> chain)
        {
            // We only verify that all the players in the list are the same, the length needs to be the length to win
            if (chain.Count != ChainForWin)
                return TicTacToePlayer.None;

            TicTacToePlayer leader = chain[0];

            foreach (var p in chain)
            {
                if (p != leader)
                    return TicTacToePlayer.None;
            }

            return leader;
        }
    }
}
