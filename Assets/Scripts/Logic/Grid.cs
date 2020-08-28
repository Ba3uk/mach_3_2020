using System;
using System.Collections;
using System.Diagnostics;

namespace Logic
{
    public delegate void GridHandlerNode(INodeView point);
    public delegate void GridHandlerPoint(Point point);
    public class Grid
    {
        public event GridHandlerPoint OnNodeDelete;
        public event GridHandlerNode OnNodeCreate;

        public int SizeX { private set; get; }
        public int SizeY { private set; get; }

        private INodeLogic[,] nodes;

        public INodeLogic this[int x, int y]
        {
            get => nodes[x,y];
            private set => nodes[x,y] = value;
        }

        public INodeLogic this[Point p] 
        {
            get => nodes[p.X, p.Y];
            private set => nodes[p.X, p.Y] = value;
        }

        public IEnumerable GetNodeViewsInfo => nodes;

        public Grid(int SizeX, int SizeY)
        {
            this.SizeX = SizeX;
            this.SizeY = SizeY;

            nodes = new Node[SizeX, SizeY];
            FillInGrid();
        }

        /// <summary>
        /// Заполняем поле
        /// </summary>
        private void FillInGrid()
        {
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    nodes[x, y] = new Node(new Point(x, y));
                }
            }
        }

        /// <summary>
        /// Проверяем - может ли объект переместиться по направлению
        /// </summary>
        private bool CanMove(Point nodePosition, MoveDiretion moveDiretion)
        {
            var targetPoint = GetTargetPoint(nodePosition, moveDiretion);

            return Contains(nodePosition.X, SizeX) && Contains(nodePosition.Y, SizeY);

            bool Contains(int value, int maxValue)
            {
                return (value >= 0 && value <= maxValue);
            }
        }

        /// <summary>
        /// Преобразуем направление в точку
        /// </summary>
        private Point GetTargetPoint(Point nodePosition, MoveDiretion moveDiretion)
        {
            switch (moveDiretion)
            {
                case (MoveDiretion.Left):
                    nodePosition.X--;
                    break;
                case (MoveDiretion.Right):
                    nodePosition.X++;
                    break;
                case (MoveDiretion.Up):
                    nodePosition.Y++;
                    break;
                case (MoveDiretion.Down):
                    nodePosition.Y--;
                    break;
            }
            return nodePosition;
        }

        /// <summary>
        /// Применяем позицию в сетке
        /// </summary>
        private void ApplyNodePosition(INodeLogic node)
        {
            nodes[node.Position.X, node.Position.Y] = node;
        }

        /// <summary>
        /// Меняем объекты местами
        /// </summary>
        public void SwapNode(INodeLogic firstNode, INodeLogic secondNode)
        {
            var firstPosition = firstNode.Position;

            firstNode.Position = secondNode.Position;
            ApplyNodePosition(firstNode);

            secondNode.Position = firstPosition;
            ApplyNodePosition(secondNode);
        }

        /// <summary>
        /// Меняем объекты местами
        /// </summary>
        public void SwapNode(Point firstNode, Point secondNode)
        {
            SwapNode(this[firstNode], this[secondNode]);
        }

        /// <summary>
        /// Меняем объекты местами по направлению
        /// </summary>
        public void SwapNode(Point nodePosition, MoveDiretion moveDiretion)
        {
            /// Сделать событие об ошибке движения

            if (CanMove(nodePosition, moveDiretion))
            {
                var targetNodePotion = GetTargetPoint(nodePosition, moveDiretion);

                var firstNode = nodes[nodePosition.X, nodePosition.Y];
                var secondNode = nodes[targetNodePotion.X, targetNodePotion.Y];

                SwapNode(firstNode, secondNode);
            }
        }

        /// <summary>
        /// Двигаем объект по сетке
        /// </summary>
        public void MoveNode(INodeLogic node, Point targetPoint)
        {
            this[node.Position] = null;
            node.Position = targetPoint;            
            ApplyNodePosition(node);
        }


        /// <summary>
        /// Сетаем новую ноду (предсозданную)  в сетку 
        /// </summary>
        /// <param name="node"></param>
        public void SetNewNode(Node node)
        {
            if (this[node.Position] != null)
            {
                throw new ArgumentOutOfRangeException();
            }

            this[node.Position] = node;
            OnNodeCreate?.Invoke(node);
        }

        /// <summary>
        /// Сетаем новую ноду в сетку
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="type"></param>
        public void SetNewNode(Point position , ColorType color , NodeType type)
        {
            if(this[position] != null)
            {
                throw new ArgumentOutOfRangeException();
            }

            this[position] = new Node(position, color, type);
            OnNodeCreate?.Invoke((INodeView)this[position]);
        }
        
        /// <summary>
        /// Удаление ноды из сетки
        /// </summary>
        /// <param name="node"></param>
        public void DeleteNode(INodeLogic node)
        {
            if (this[node.Position] != null)
            {
                OnNodeDelete?.Invoke(node.Position);
                this[node.Position].MachIt();
                this[node.Position] = null;
            }
        }

        /// <summary>
        /// Очистка ноды по координатам
        /// </summary>
        /// <param name="position"></param>
        public void DeleteNode(Point position)
        {
            if (this[position] != null)
            {
                OnNodeDelete?.Invoke(position);
                this[position].MachIt();
                this[position] = null;

            }
        }

        public void DeleteNodes(INodeLogic[] nodes)
        {
            foreach (var node in nodes) DeleteNode(node);
        }
    }
}

