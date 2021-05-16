using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Movement movement;
    private BeetlesManager beetlesManager;
    [SerializeField] private bool inverseDirection = false;

    private void Awake()
    {
        movement = GetComponentInParent<Movement>();
        beetlesManager = FindObjectOfType<BeetlesManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Node"))
        {
            if(other.GetComponentInParent<Movement>().isActivePath)
            {
                
                if(!movement.isActivePath)
                {
                    movement.inverted = inverseDirection;
                    
                }
                if(movement == beetlesManager.GetInitialMovement() && beetlesManager.GetCloseAllowance() == 1)
                {
                    other.GetComponentInParent<Movement>().inverted = true; 
                }
                movement.SetEntryPoint(this.transform);
                beetlesManager.AddToActivePath(movement);
            }
        }
    }
}
