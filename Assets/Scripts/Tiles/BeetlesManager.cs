using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PathState {Moving, Closed, Dead}

public class BeetlesManager : MonoBehaviour
{
    private Queue<Movement> activePath =  new Queue<Movement>();
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
        if(movement == initialMovement && closePathAllowed < 1)
        {
            closePathAllowed ++;
        }

        else if(movement == initialMovement && closePathAllowed == 1)
        {
            state = PathState.Closed;
        }

        else if(!movement.isActivePath)
        {
            activePath.Enqueue(movement);
            movement.isActivePath = true;
        }
    }

    private void Update()
    {
        Debug.Log(state);
        
        if(activePath.Count == 0  && state == PathState.Closed)
        {
            StartCoroutine(WaitForLoop(beetle));
        }

        if(activePath.Count == 0 && state == PathState.Dead)
        {
            StopAllCoroutines();
            SceneManager.LoadScene("Derrota");
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

        if(activePath.Count == 0)
        {
            state = PathState.Dead;
        }
    }

    private IEnumerator WaitForLoop(Beetle beetle)
    {
        yield return new WaitForSecondsRealtime(beetle.speed + 0.2f);
        SceneManager.LoadScene("Vitoria");
    }

    public Movement GetInitialMovement()
    {
        return initialMovement;
    }

    public int GetCloseAllowance()
    {
        return closePathAllowed;
    }
}
