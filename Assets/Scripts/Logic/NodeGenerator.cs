
using Assets.Scripts.Game;
using FMS;

namespace Logic
{
    public class NodeGenerator : IState
    {
        private Grid grid;


        private void GenerateUp()
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                for (int y = grid.SizeY - 1; y > 0; y--)
                {
                    if (!CanContinue(x, y)) break;

                    Create(x, y);
                }
            }
        }

        private void GenerateDown()
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                for (int y = 0; y < grid.SizeY; y++)
                {
                    if (!CanContinue(x,y)) break;

                    Create(x, y);
                }
            }
        }

        /// <summary>
        /// Можем ли мы начать создание ноды в клетке
        /// </summary>
        private bool CanContinue(int x, int y) => grid[x, y] == null;

        /// <summary>
        /// Создаем ноду
        /// </summary>
        private void Create(int x , int y)
        {
            grid.SetNewNode(new Node(new Point(x, y)));
        }

        public void Enter(Grid grid)
        {
            this.grid = grid;

            if (GravityModule.IsNormalGravity)
            {
                GenerateUp();
            }
            else
            {
                GenerateDown();
            }

            GameMaker.Instance.Machine.SetState(new FindMach(), 1f);
        }

        public void Execute() { }

        public void Exit() { }
    }
}
