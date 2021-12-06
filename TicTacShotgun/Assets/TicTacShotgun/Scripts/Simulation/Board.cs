using System;
using TicTacShotgun.GameFlow;
using TicTacShotgun.Utils;
using UnityEngine;

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
        public const int BOARD_SIZE = 3;

        public event Action<Move> OnMovePerformed = m => { };
        public event Action<Move> OnUndoMovePerformed = m => { };
        public bool NextMoveAvailable => performedMovesCount < fieldsCount;
        public int FieldsCount => fieldsCount;
        public int OccupiedFieldsCount => performedMovesCount;
        public int UnoccupiedFieldsCount => fieldsCount - performedMovesCount;
        
        public Board()
        {
            board = new int[BOARD_SIZE, BOARD_SIZE];
            fieldsCount = BOARD_SIZE * BOARD_SIZE;
        }
        
        public bool IsMoveValid(Move move)
        {
            return IsMoveWithinBoundaries(move) && this[move.Index] == EMPTY_FIELD;
        }

        public void MakeMove(Move move)
        {
            if (!IsMoveValid(move))
            {
                TicTacLogger.LogError("Move not allowed");
                return;
            }

            this[move.Index] = move.Player.Index;
            performedMovesCount++;

            var boardString = string.Empty;

            for (int y = 0; y < BOARD_SIZE; y++)
            {
                for (int x = 0; x < BOARD_SIZE; x++)
                {
                    boardString += $"{board[x, y] }";
                }

                boardString += "\n";
            }
            
            Debug.Log($"Player {move.Player.Index}: {move.Index}\n{boardString}");

            OnMovePerformed.Invoke(move);
        }

        public void UndoMove(Move move)
        {
            if (!IsMoveWithinBoundaries(move))
            {
                return;
            }

            this[move.Index] = EMPTY_FIELD;
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

        public Index GetBoardIndex(int x, int y)
        {
            if (x < BOARD_SIZE && y < BOARD_SIZE)
            {
                return new Index(x, y);
            }

            TicTacLogger.LogError("Tried to get board index outside of bounds!");
            return default;
        }
        
        bool IsMoveWithinBoundaries(Move move)
        {
            return move.Index.X < BOARD_SIZE && move.Index.Y < BOARD_SIZE;
        }

        int this[Index index]
        {
            get => board[index.X, index.Y];
            set => board[index.X, index.Y] = value;
        }
        
        public readonly struct Index : IComparable<Index>
        {
            public readonly int X;
            public readonly int Y;

            public Index(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int CompareTo(Index other)
            {
                var xComparison = X.CompareTo(other.X);
                
                if (xComparison != 0)
                {
                    return xComparison;
                }
                
                return Y.CompareTo(other.Y);
            }

            public override string ToString()
            {
                return $"{X}, {Y}";
            }
        }
    }
}