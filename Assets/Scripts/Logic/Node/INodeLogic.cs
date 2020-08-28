namespace Logic
{
    public interface INodeLogic
    {
        Point Position { set; get; }

        ColorType Color { get; }

        NodeType Type { get; }

        void MachIt();
    }
}
