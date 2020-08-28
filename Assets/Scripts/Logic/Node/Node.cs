using System;

namespace Logic
{
    public delegate void NodeHandelr(Point point);
   
    public class Node : INodeLogic , INodeView
    {
        public event NodeHandelr OnMoveNode;
        public event Action OnPointMach;

        private Point position;
        
        /// <summary>
        /// Позиция объекта
        /// </summary>
        public Point Position 
        { 
            set
            {
                if(position != value)
                {
                    position = value;
                    OnMoveNode?.Invoke(position);
                }
            }

            get => position;
        }

        /// <summary>
        /// Цвет объекта
        /// </summary>
        public ColorType Color { private set;  get; }

        /// <summary>
        /// Тип объекта
        /// </summary>
        public NodeType Type { private set; get; }

        public Node(Point position , ColorType color , NodeType type)
        {
            Position = position;
            Color = color;
            Type = type;
        }

        public Node(Point position): this(position , ColorHelper.GetRandomColor() , NodeType.Normal)
        {
        }

        public void MachIt()
        {
            OnPointMach?.Invoke();
        }
    }
}
