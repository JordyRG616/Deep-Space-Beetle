using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementProcessor : MonoBehaviour
{
    private MovementTemplate movement;
    private NodeComponent[] nodes = new NodeComponent[3];

    private void Awake()
    {
        movement = GetComponent<MovementTemplate>();
    }

    public void SetNodeList(NodeComponent startNode, NodeComponent midNode, NodeComponent endNode)
    {
        nodes[0] = startNode;
        nodes[1] = midNode;
        nodes[2] = endNode;
    }

    public void AccessMovement(Transform target, float movementTime)
    {
        movement.DoMovement(target, nodes, movementTime);
    }

    public bool IsMovementDone()
    {
        return movement.isFinished;
    }
}
