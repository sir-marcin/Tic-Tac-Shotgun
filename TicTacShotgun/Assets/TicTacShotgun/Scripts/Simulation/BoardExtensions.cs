using TicTacShotgun.GameFlow;

namespace TicTacShotgun.Simulation
{
    public static class BoardExtensions
    {
        public static GameBoardState EvaluateCurrentBoardState(this Board board)
        {
            var boardArray = board.GetCurrentBoardArray();
            var boardSize = board.BOARD_SIZE;
            var sameValuesInRowCount = 1;

            if (boardArray[0, 0] != Board.EMPTY_FIELD)
            {
                // diagonal down
                for (int currIndex = 1, prevIndex = currIndex - 1; currIndex < boardSize; currIndex++)
                {
                    if (boardArray[currIndex, currIndex] != boardArray[prevIndex, prevIndex])
                    {
                        break;
                    }

                    sameValuesInRowCount++;
                }
                
                if (sameValuesInRowCount == boardSize)
                {
                    return GameBoardState.Win;
                }
            }

            if (boardArray[0, boardSize - 1] != Board.EMPTY_FIELD)
            {
                // diagonal up
                for (int x = 1, y = boardSize - 1 - x; x < boardSize; x++, y--)
                {
                    if (boardArray[x, y] != boardArray[x - 1, y + 1])
                    {
                        break;
                    }

                    sameValuesInRowCount++;
                }
                
                if (sameValuesInRowCount == boardSize)
                {
                    return GameBoardState.Win;
                }
            }
            
            for (int y = 0; y < boardSize; y++)
            {
                if (boardArray[0, y] == Board.EMPTY_FIELD)
                {
                    continue;
                }
                
                sameValuesInRowCount = 1;
                
                for (int x = 1; x < boardSize; x++)
                {
                    if (boardArray[x, y] != boardArray[x - 1, y])
                    {
                        break;
                    }

                    sameValuesInRowCount++;
                }

                if (sameValuesInRowCount == boardSize)
                {
                    return GameBoardState.Win;
                }
            }
            
            for (int x = 0; x < boardSize; x++)
            {
                if (boardArray[x, 0] == Board.EMPTY_FIELD)
                {
                    continue;
                }
                
                sameValuesInRowCount = 1;
                
                for (int y = 1; y < boardSize; y++)
                {
                    if (boardArray[x, y] != boardArray[x, y - 1])
                    {
                        break;
                    }

                    sameValuesInRowCount++;
                }

                if (sameValuesInRowCount == boardSize)
                {
                    return GameBoardState.Win;
                }
            }

            return GameBoardState.GameInProgress;
        }
    }
}