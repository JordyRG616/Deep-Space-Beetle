using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetlesManager : MonoBehaviour
{
    private Queue<Movement> activePath =  new Queue<Movement>();
    [SerializeField] private Beetle beetle;
    public int activePathSize;
    [SerializeField] private Movement initialMovement;

    private void Awake()
    {
        activePath.Enqueue(initialMovement);
        initialMovement.isActivePath = true;
        StartCoroutine(MoveBeetle(beetle));
    }

    public void AddToActivePath(Movement movement)
    {
        if(movement == initialMovement)
        {
            movement = null;
        }

        else if(!activePath.Contains(movement))
        {
            activePath.Enqueue(movement);
            movement.isActivePath = true;
        }
    }

    private void Update()
    {
        activePathSize = activePath.Count;
    }

    private IEnumerator MoveBeetle(Beetle beetle)
    {
        while(activePath.Count > 0)
        {
            Movement activeMovement = activePath.Dequeue();
            activeMovement.DoMovement(beetle.transform, beetle.speed);
            
            AddToActivePath(activeMovement);
            
            yield return new WaitUntil(() => activeMovement.isFinished == true);
        }
    }
}
