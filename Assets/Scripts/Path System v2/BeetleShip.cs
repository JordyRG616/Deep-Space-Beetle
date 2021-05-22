using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleShip : MonoBehaviour
{
    private Queue<MovementProcessor> movements = new Queue<MovementProcessor>();
    private Queue<MovementProcessor> loop = new Queue<MovementProcessor>();
    [SerializeField] private float travelTime;
    private bool loopAllowed = false;
    public bool loopClosed = false;

    public void AddToQueue(MovementProcessor movement)
    {
        if(!loop.Contains(movement) && !movements.Contains(movement))
        {
            movements.Enqueue(movement);
        }
    }

    private IEnumerator Move()
    {
        while(movements.Count > 0)
        {
            MovementProcessor activeMovement = movements.Dequeue();
            activeMovement.AccessMovement(this.transform, travelTime);
            loop.Enqueue(activeMovement);
            
            yield return new WaitUntil(() => activeMovement.IsMovementDone() == true);
        }

        if(loopClosed) { loopAllowed = true;}
        else { Die(); }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    public void StartLoop()
    {
        //travelTime /= 5;
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        yield return new WaitUntil(() => loopAllowed == true);

        StopCoroutine(Move());


        while(loop.Count > 0)
        {
            MovementProcessor activeMovement = loop.Dequeue();
            activeMovement.AccessMovement(this.transform, travelTime);
            
            yield return new WaitUntil(() => activeMovement.IsMovementDone() == true);
            
            loop.Enqueue(activeMovement);
        }
    }
}
