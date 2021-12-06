using TicTacShotgun.BoardView;
using TicTacShotgun.PlayerInput;

namespace TicTacShotgun.Simulation
{
    public class HumanLocalPlayer : Player
    {
        Board board;
        
        public HumanLocalPlayer(int index, Board board) : base(index)
        {
            this.board = board;
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
            Move(boardField);
        }
    }
}