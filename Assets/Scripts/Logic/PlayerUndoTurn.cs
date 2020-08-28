using Assets.Scripts.Game;
using FMS;

namespace Logic
{
    public class PlayerUndoTurn : IState
    {
        private Logic.Grid grid;

        public void Enter(Grid grid)
        {
            this.grid = grid;

            var lastInput = InputLog.DeleteLastLog();

            grid.SwapNode(lastInput.firstPoint, lastInput.secondPoint);

            GameMaker.Instance.Machine.SetState(new PlayerTurn(), 1f);
        }

        public void Execute() {}

        public void Exit() {}
    }
}
