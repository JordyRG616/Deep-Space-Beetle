using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTriggerEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        BeetleMovement beetle = other.GetComponent<BeetleMovement>();
        if(beetle != null)
        {
            beetle.StopAllCoroutines();
        }
    }
}
