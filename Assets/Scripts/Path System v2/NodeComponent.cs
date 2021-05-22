using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeComponent : MonoBehaviour
{
    [SerializeField] private NodeComponent midNode, endNode;
    private MovementProcessor processor;
    public bool isActiveNode = false;
    private BeetleShip ship;
    private NodeComponent otherNode;
    public bool isInitialNode;

    private void Awake()
    {
        
    }

    private void PassNodes(NodeComponent node1, NodeComponent node2, NodeComponent node3)
    {
        processor.SetNodeList(node1, node2, node3);
        isActiveNode = true;
        midNode.isActiveNode = true;
        midNode.SetBeetleShip(ship);
        endNode.isActiveNode = true;
        endNode.SetBeetleShip(ship);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        processor = GetComponentInParent<MovementProcessor>();
        ship = FindObjectOfType<BeetleShip>();

        if(other.gameObject.CompareTag("Node") && !gameObject.CompareTag("InitialNode"))
        {
            otherNode = other.GetComponent<NodeComponent>();

            if(otherNode.isActiveNode && !isActiveNode)
            {
                ActivateMovement(gameObject.GetComponent<NodeComponent>(), midNode, endNode);
            }
            else
            {
                StartCoroutine(WaitForActivation());
            }
        } else if(other.gameObject.CompareTag("InitialNode") && isActiveNode)
        {
            ship.loopClosed = true;
            ship.StartLoop();
        }
    }

    private IEnumerator WaitForActivation()
    {
        yield return new WaitUntil(() => otherNode.isActiveNode == true);

        ActivateMovement(endNode, midNode, gameObject.GetComponent<NodeComponent>());

        StopCoroutine(WaitForActivation());
    }

    public void ActivateMovement(NodeComponent node1, NodeComponent node2, NodeComponent node3)
    {
        PassNodes(node1, node2, node3);
        ship.AddToQueue(processor);
    }

    public void SetBeetleShip(BeetleShip beetleShip)
    {
        ship = beetleShip;
    }
}
