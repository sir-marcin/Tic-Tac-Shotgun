using System;
using TicTacShotgun.GameFlow;
using TicTacShotgun.Utils;

namespace TicTacShotgun.Simulation
{
    public class Board
    {
        /* I could use byte table to save memory, but:
         *  - game board is small
         *  - AI simulations won't get deep
         *  - any future dev would need to specifically make sure to use byte in relevant operations to avoid implicit casting 
         * 
         * For this scenario using int will make things simpler.
         */
        readonly int[,] board;
        readonly int fieldsCount;
        int performedMovesCount = 0;
        
        public const int EMPTY_FIELD = 0;
        public readonly int BOARD_SIZE = 3;
        
        public event Action<Move> OnMovePerformed = m => { };
        public event Action<Move> OnUndoMovePerformed = m => { };
        public bool NextMoveAvailable => performedMovesCount < fieldsCount;

        public Board()
        {
            board = new int[BOARD_SIZE, BOARD_SIZE];
            fieldsCount = BOARD_SIZE * BOARD_SIZE;
        }
        
        public bool IsMoveValid(Move move)
        {
            return IsMoveWithinBoundaries(move) && board[move.X, move.Y] == EMPTY_FIELD;
        }

        public void MakeMove(Move move)
        {
            if (!IsMoveValid(move))
            {
                TicTacLogger.LogError("Move not allowed");
                return;
            }

            board[move.X, move.Y] = move.Player.Index;
            performedMovesCount++;
            
            OnMovePerformed.Invoke(move);
        }

        public void UndoMove(Move move)
        {
            if (!IsMoveWithinBoundaries(move))
            {
                return;
            }

            board[move.X, move.Y] = EMPTY_FIELD;
            performedMovesCount--;
            
            OnUndoMovePerformed.Invoke(move);
        }
        
        /// <summary>
        /// Returns a copy of current board.
        /// </summary>
        /// <returns>Deep copy of board array</returns>
        public int[,] GetCurrentBoardArray()
        {
            var boardClone = new int[BOARD_SIZE, BOARD_SIZE];
            Array.Copy(board, boardClone, BOARD_SIZE * BOARD_SIZE);

            return boardClone;
        }

        bool IsMoveWithinBoundaries(Move move)
        {
            return move.X < BOARD_SIZE && move.Y < BOARD_SIZE;
        }

        bool IsGameFinished(out GameBoardState? result)
        {
            result = null;

            return false;
        }
    }
}