using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Movement movement;
    private BeetlesManager beetlesManager;

    private void Awake()
    {
        movement = GetComponentInParent<Movement>();
        beetlesManager = FindObjectOfType<BeetlesManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponentInParent<Movement>().isActivePath)
        {
            movement.SetEntryPoint(this.transform);
            beetlesManager.AddToActivePath(movement);
        }
    }
}
