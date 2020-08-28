using Assets.Scripts.Game;
using FMS;
using System.Collections.Generic;

namespace Logic
{
    public class FindMach: IState
    {
        private Grid grid;
        private bool isPlayerMach;

        public FindMach(bool isPlayerMach = false)
        {
            this.isPlayerMach = isPlayerMach;
        }

        public void Enter(Grid grid)
        {
            this.grid = grid;

            Find(out List<INodeLogic[]> machNodes);
            if(machNodes.Count > 0)
            {
                GameMaker.Instance.Machine.SetState(new ApplyMach(machNodes));
            }
            else
            {
                if (isPlayerMach && InputLog.HasLog)
                    GameMaker.Instance.Machine.SetState(new PlayerUndoTurn());
                else
                    GameMaker.Instance.Machine.SetState(new PlayerTurn()); 
            }
        }

        public void Execute() { }

        public void Exit() { }

 
        public void Find(out List<INodeLogic[]> machNodes)
        {
            machNodes = new List<INodeLogic[]>();

            List<INodeLogic> buffer = new List<INodeLogic>();
            ColorType firstColor;

            //Совпадения по Y
            for (int x = 0; x < grid.SizeX; x++)
            {
                CheckAndClearBuffer(ref machNodes);
                firstColor = grid[x, 0].Color;

                for (int y = 0; y < grid.SizeY; y++)
                {
                    //Если цвета совпадают считаем их
                    if (firstColor == grid[x, y].Color)
                    {
                        buffer.Add(grid[x, y]);
                    }
                    else
                    {
                        //Совоподения закончились , смотрим сколько было совпадаений и если достаточно - добавляем их
                        CheckAndClearBuffer(ref machNodes);

                        firstColor = grid[x, y].Color;
                        buffer.Add(grid[x, y]);
                    }
                }
            }

            //Сортировка по X
            for (int y = 0; y < grid.SizeY; y++)
            {
                CheckAndClearBuffer(ref machNodes);
                firstColor = grid[0, y].Color;

                for (int x = 0; x < grid.SizeX; x++)
                {
                    //Если цвета совпадают считаем их
                    if (firstColor == grid[x, y].Color)
                    {
                        buffer.Add(grid[x, y]);
                    }
                    else
                    {
                        //Совоподения закончились , смотрим сколько было совпадаений и если достаточно - добавляем их
                        CheckAndClearBuffer(ref machNodes);

                        firstColor = grid[x, y].Color;
                        buffer.Add(grid[x, y]);
                    }
                }
            }

            //Проверяем буфер и очищаем его
            void CheckAndClearBuffer(ref List<INodeLogic[]> nodes)
            {
                if (buffer.Count >= 3)
                {
                    nodes.Add(buffer.ToArray());
                }

                buffer.Clear();
            }
        }

        
    }
}
