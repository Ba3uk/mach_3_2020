using Assets.Scripts.Game;
using FMS;
using System.Collections.Generic;

namespace Logic
{
    public class ApplyMach: IState
    {
        private Grid grid;
        private List<INodeLogic[]> allMachs;
        public ApplyMach(List<INodeLogic[]> allMachs)
        {
            this.allMachs = allMachs;
        }

        private void Apply()
        {
            foreach (var mach in allMachs)
            {
                if(mach.Length > 3)
                {
                    var firstNodePos = mach[0].Position;
                    var color = mach[0].Color;

                    MachNode(mach);
                    grid.SetNewNode(firstNodePos, color, NodeType.Gravity);
                }
                else
                {
                    MachNode(mach);
                }
            }
        }


        /// <summary>
        /// Мачим ноды, если они имеют специфичное поведение - информирует обработчиков поведения 
        /// </summary>
        private void MachNode(INodeLogic[] nodes)
        {
            foreach (var node in nodes)
            {
                switch (node.Type)
                {
                    case (NodeType.Gravity):
                        GameEventManager.MachGravityNode?.Invoke();
                        break;
                }
            }

            grid.DeleteNodes(nodes);
        }

        public void Enter(Grid grid)
        {
            this.grid = grid;
            Apply();

            GameMaker.Instance.Machine.SetState(new NodeGravity());
        }

        public void Execute() { }

        public void Exit() { }


    }
}