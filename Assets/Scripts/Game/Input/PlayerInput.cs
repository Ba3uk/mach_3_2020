using Assets.Scripts.Game;
using GameCore;
using Logic;
using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Logic.Grid grid;
    private NodeView firstNode;
    private NodeView secondNode;

    private void Awake()
    {
        StateMachine.OnChangeCurrentState += SetActive;
    }

    private void OnDestroy()
    {
        StateMachine.OnChangeCurrentState -= SetActive;
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            ChekNode(ref firstNode);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (firstNode)
            {
                ChekNode(ref secondNode);

                if (secondNode && firstNode)
                {
                    if (CanSwapNode(firstNode, secondNode))
                        GameEventManager.PlayerSwipeNode.Invoke(firstNode.View.Position, secondNode.View.Position);
                    else
                        ResetNode();
                }
            }else
                ResetNode();
        }
    }
    private void ResetNode()
    {
        if (firstNode) firstNode.SelectNode(false);
        if (secondNode) secondNode.SelectNode(false);
        firstNode = null;
        secondNode = null;
    }

    private bool CanSwapNode(NodeView first , NodeView second)
    {
        int differenceX = System.Math.Abs( first.View.Position.X - secondNode.View.Position.X);
        int differenceY = System.Math.Abs(first.View.Position.Y - secondNode.View.Position.Y);

        return differenceX + differenceY == 1;
    }

    private void ChekNode(ref NodeView node)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit)
        {
            node = hit.transform.GetComponent<NodeView>();
            if (node)
                node.SelectNode(true);
        }
    }

    private void SetActive(FMS.IState obj)
    {
        gameObject.SetActive(obj is PlayerTurn);
        ResetNode();
    }
}
