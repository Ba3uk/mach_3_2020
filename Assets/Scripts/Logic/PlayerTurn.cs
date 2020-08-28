using Assets.Scripts.Game;
using FMS;

namespace Logic
{
    public class PlayerTurn : IState
    {
        private Logic.Grid grid;

        public void Enter(Grid grid)
        {
            this.grid = grid;
            GameEventManager.PlayerSwipeNode.Subscribe(OnPlayerTurn);
        }

        private void OnPlayerTurn(Point firstNode, Point secondNode)
        {
            grid.SwapNode(firstNode, secondNode);
            GameMaker.Instance.Machine.SetState(new FindMach(true), 1f); 
        }

        public void Execute() {}

        public void Exit()
        {
            GameEventManager.PlayerSwipeNode.UnSubscribe(OnPlayerTurn);
        }
    }
}
