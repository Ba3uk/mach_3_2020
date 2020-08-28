using Assets.Scripts.Game;
using FMS;

namespace Logic
{
    public class NodeGravity : IState
    {
        private Grid grid;


        public void Enter(Grid grid)
        {
            this.grid = grid;

            if (GravityModule.IsNormalGravity)
            {
                ApplyGravity();
            }
            else
            {
                ApplyGravityreverse();
            }

            GameMaker.Instance.Machine.SetState(new NodeGenerator(), 1f);
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }


        private void ApplyGravityreverse()
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                INodeLogic logic;
                for (int y = grid.SizeY - 1; y >= 1; y--)
                {
                    for (int j = y - 1; j >= 0; j--)
                    {

                        if (grid[x, y] == null && grid[x, j] != null)
                        {
                            grid.MoveNode(grid[x, j], new Point(x, y));
                        }
                    }
                }
            }
        }

        private void ApplyGravity()
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                INodeLogic logic;
                for (int y = 0; y < grid.SizeY; y++)
                {
                    for (int j = y + 1; j < grid.SizeY; j++)
                    {

                        if (grid[x, y] == null && grid[x, j] != null)
                        {
                            grid.MoveNode(grid[x, j], new Point(x, y));
                        }
                    }
                }
            }
        }
    }
    



}
