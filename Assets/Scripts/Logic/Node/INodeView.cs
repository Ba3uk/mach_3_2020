using System;

namespace Logic
{
    public interface INodeView
    {
        Point Position { get; }

        ColorType Color { get; }

        NodeType Type { get; }

        event NodeHandelr OnMoveNode;
        event Action OnPointMach;
    }
}
