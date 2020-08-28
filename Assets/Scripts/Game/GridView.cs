using UnityEngine;
using Logic;

namespace GameCore
{
    public class GridView : MonoBehaviour
    {
        private Transform startPoint;

        public Transform StartPoint => startPoint;

        private Logic.Grid grid;
        [SerializeField] private GameObject borderPref;
        [SerializeField] private NodeView nodePref;

        public void InitGrid(Logic.Grid grid)
        {
            this.grid = grid;
            InitStartPoint();
            InitBorder(grid.SizeX, grid.SizeY);
            InitAllNode();

            grid.OnNodeCreate += CreateNode;
        }

        /// <summary>
        /// Cоздание визуальных нод
        /// </summary>
        /// <param name="node"></param>
        private void CreateNode(INodeView node)
        {
            Instantiate(nodePref, startPoint).Init(node);
        }

        /// <summary>
        /// Создание точки отчета координат для создания игры
        /// </summary>
        private void InitStartPoint()
        {
            startPoint = new GameObject().transform;
            startPoint.name = "StartPoint";
            startPoint.position = Vector2.one;
        }

        /// <summary>
        /// Создание бордера для нод
        /// </summary>
        private void InitBorder(int xSize, int ySize)
        {
            var borderParent = new GameObject().transform;
            borderParent.name = "BorderParent";

            for (int x = 0;  x <= xSize + 1; x++)
            {
                var go = Instantiate(borderPref, borderParent);
                go.transform.position = new Vector2(x, ySize +1);

                go = Instantiate(borderPref, borderParent);
                go.transform.position = new Vector2(x,0);
            }

            for (int y = 0; y <= ySize + 1; y++)
            {
                var go = Instantiate(borderPref, borderParent);
                go.transform.position = new Vector2(xSize +1, y);

                go = Instantiate(borderPref, borderParent);
                go.transform.position = new Vector2(0, y);
            }
        }

        /// <summary>
        /// первичная генерация нод
        /// </summary>
        private void InitAllNode()
        {
            foreach (Node node in grid.GetNodeViewsInfo)
            {
                CreateNode(node);
            }
        }
    }
}
