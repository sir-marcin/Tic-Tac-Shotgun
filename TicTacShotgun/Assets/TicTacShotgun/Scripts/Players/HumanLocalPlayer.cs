using TicTacShotgun.BoardView;
using TicTacShotgun.PlayerInput;
using TicTacShotgun.Simulation;

namespace TicTacShotgun.Players
{
    public class HumanLocalPlayer : Player
    {
        public HumanLocalPlayer(int index, Board board) : base(index, board)
        {
        }

        public override void OnTurnStart()
        {
            BoardMouseInput.OnClick += HandleBoardFieldClick;
        }

        public override void OnTurnEnd()
        {
            BoardMouseInput.OnClick -= HandleBoardFieldClick;
        }

        void HandleBoardFieldClick(BoardField boardField)
        {
            RequestMove(boardField);
        }
    }
}