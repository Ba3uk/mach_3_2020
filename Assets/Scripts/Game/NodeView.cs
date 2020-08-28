using Logic;
using UnityEngine;

namespace GameCore
{
    public class NodeView : MonoBehaviour
    {

        public INodeView View
        {
            private set;
            get;
        }

        private Vector3 targetValue;

        public SpriteRenderer image;

        public void Init(INodeView node)
        {
            View = node;
            transform.localPosition = new Vector2(View.Position.X , View.Position.Y);

            SetColor(View.Color);
            SetImage(View.Type);

            View.OnMoveNode += OnMoveNode;
            View.OnPointMach += node_OnPointMach;

            //Test
            targetValue = transform.localPosition;
        }

        public void SelectNode(bool isSelected)
        {
            transform.localScale = Vector3.one * (isSelected ? 1.2f : 1f); 
        }

        private void FixedUpdate()
        {
            if(transform.localPosition != targetValue)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetValue, 5f * Time.fixedDeltaTime);
            }
        }

        private void SetColor(ColorType color)
        {
            Color uColor;
            ColorUtility.TryParseHtmlString(ColorHelper.GetColorCode(color), out uColor);
            image.color = uColor;
        }

        private void SetImage(NodeType type)
        {
            image.sprite = ArtCollection.Instance.ArtConfig.GetNodeImageByType(View.Type);
        }

        private void node_OnPointMach()
        {
            Destroy(gameObject);
            DeInit();
        }

        private void OnMoveNode(Point point)
        {
            targetValue = new Vector2(point.X, point.Y);
        }

        public void DeInit()
        {
            View.OnMoveNode -= OnMoveNode;
            View.OnPointMach -= node_OnPointMach;
            View = null;
            
        }
    }
}