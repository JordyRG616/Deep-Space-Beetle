using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialNode : NodeComponent
{
    [SerializeField] private BeetleShip ship;


    void OnTriggerEnter2D(Collider2D other)
    {
        ship = FindObjectOfType<BeetleShip>();
        
        if(other.gameObject.CompareTag("Node"))
        {
            if(other.GetComponent<NodeComponent>().isActiveNode)
            {
                ship.loopClosed = true;
                ship.StartLoop();
            }
        }
    }
}
