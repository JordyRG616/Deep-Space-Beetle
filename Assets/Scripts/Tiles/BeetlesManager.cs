using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathState {Moving, Closed, Dead}

public class BeetlesManager : MonoBehaviour
{
    private Queue<Movement> activePath =  new Queue<Movement>();
    private Queue<Movement> finalPath = new Queue<Movement>();
    [SerializeField] private Beetle beetle;
    [SerializeField] private Movement initialMovement;
    [SerializeField] private Transform nodePosition;
    private PathState state = PathState.Moving;
    private int closePathAllowed = 0;

    private void Awake()
    {
        //beetle = GetComponent<Beetle>();

        initialMovement.SetEntryPoint(nodePosition);
        activePath.Enqueue(initialMovement);
        initialMovement.isActivePath = true;
        StartCoroutine(MoveBeetle(beetle));
    }

    public void AddToActivePath(Movement movement)
    {
        if(movement == initialMovement && closePathAllowed < 2)
        {
            closePathAllowed ++;
        }

        else if(movement == initialMovement && closePathAllowed == 2)
        {
            state = PathState.Closed;
        }

        else if(!movement.isActivePath)
        {
            activePath.Enqueue(movement);
            finalPath.Enqueue(movement);
            movement.isActivePath = true;
        }
    }

    private void Update()
    {
        if(activePath.Count <= 0 && state == PathState.Closed)
        {
            StopAllCoroutines();
            StartCoroutine(LoopForever(beetle));
        }

        if(activePath.Count <= 0 && state == PathState.Dead)
        {
            StopAllCoroutines();
            Destroy(beetle.gameObject);
        }
    }

    private IEnumerator MoveBeetle(Beetle beetle)
    {
        while(activePath.Count > 0)
        {
            Movement activeMovement = activePath.Dequeue();
            activeMovement.DoMovement(beetle.transform, beetle.speed);
            
            

            yield return new WaitUntil(() => activeMovement.isFinished == true);

        }
    }

    private IEnumerator LoopForever(Beetle beetle)
    {
        while(state == PathState.Closed)
        {
            Movement activeMovement = finalPath.Dequeue();
            activeMovement.DoMovement(beetle.transform, beetle.speed);

            yield return new WaitUntil(() => activeMovement.isFinished == true);                

            finalPath.Enqueue(activeMovement);
        }
    }
}
