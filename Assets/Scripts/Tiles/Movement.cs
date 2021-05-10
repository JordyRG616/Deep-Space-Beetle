using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] MovementType type;
    [SerializeField] private Transform[] nodes;
    private float totalTime;
    private Transform entryPoint;
    private float elapsedTime;
    public bool isActivePath = false;
    public bool isFinished = false;
    public bool inverted;

    public void DoMovement(Transform target, float time)
    {
        totalTime = time;

        switch(type)
        {
            case MovementType.Straight:

                if(inverted == false)
                {
                    StartCoroutine(StraightMove(target, nodes[0], nodes[1]));
                } else if(inverted == true)
                  {
                    StartCoroutine(StraightMove(target, nodes[1], nodes[0]));
                  }
                break;

            case MovementType.Curve:

                if(inverted == false)
                {
                    StartCoroutine(CurveMove(target, nodes[0], nodes[1], nodes[2]));
                } else if(inverted == true)
                    {
                        StartCoroutine(CurveMove(target, nodes[2], nodes[1], nodes[0]));
                    }
                break;

            case MovementType.Turn:

                if(inverted == false)
                {
                    StartCoroutine(TurnMove(target, nodes[0], nodes[1], nodes[2]));
                } else if(inverted == true)
                    {
                        StartCoroutine(TurnMove(target, nodes[2], nodes[1], nodes[0]));
                    }
                break;
        }
    }

    private IEnumerator StraightMove(Transform target, Transform pointA, Transform pointB)
    {
        isFinished = false;

        AdjustRotation(target, pointA.position, pointB.position);


        while(elapsedTime <= totalTime)
        {
            Vector3 move = Vector3.Lerp(pointA.position, pointB.position, elapsedTime / totalTime);
            Vector3 nextPoint = Vector3.LerpUnclamped(pointA.position, pointB.position, (elapsedTime / totalTime) + 0.1f);

            target.transform.position = move;
            
            yield return new WaitForSecondsRealtime(0.01f);
            elapsedTime += 0.01f;
        }

        isFinished = true;
        StopCoroutine("StraightMove");
    }

    private IEnumerator CurveMove(Transform target, Transform pointA, Transform pointB, Transform pointC)
    {
        isFinished = false;
        while(elapsedTime <= totalTime)
        {
            Vector3 movingPointA = Vector3.Lerp(pointA.position, pointB.position, elapsedTime / totalTime);
            Vector3 MovingPointB = Vector3.Lerp(pointB.position, pointC.position, elapsedTime / totalTime);

            Vector3 move = Vector3.Lerp(movingPointA, MovingPointB, elapsedTime / totalTime);
            Vector3 nextPoint = Vector3.LerpUnclamped(movingPointA, MovingPointB, (elapsedTime / totalTime) + 0.1f);

            target.transform.position = move;

            AdjustRotation(target, target.transform.position, nextPoint);

            yield return new WaitForSecondsRealtime(0.01f);
            elapsedTime += 0.01f;
        }

        isFinished = true;
        elapsedTime = 0;
        StopCoroutine("CurveMove");
    }

    private IEnumerator TurnMove(Transform target, Transform pointA, Transform pointB, Transform pointC)
    {
        isFinished = false;

        AdjustRotation(target, pointA.position, pointB.position);

        while(elapsedTime <= totalTime * 0.75f)
        {
            Vector3 move = Vector3.Lerp(pointA.position, pointB.position, elapsedTime / (totalTime * 0.75f));
            Vector3 nextPoint = Vector3.LerpUnclamped(pointA.position, pointB.position, (elapsedTime / totalTime * 0.75f) + 0.1f);

            target.transform.position = move;

            //AdjustTurnRotation(target, transform.position, pointB.position);

            yield return new WaitForSecondsRealtime(0.01f);
            elapsedTime += 0.01f;
        }

        elapsedTime = 0f;

        AdjustRotation(target, pointB.position, pointC.position);

        while(elapsedTime <= totalTime *0.25f)
        {
            Vector3 move = Vector3.Lerp(pointB.position, pointC.position, elapsedTime / (totalTime * 0.25f));
            Vector3 nextPoint = Vector3.LerpUnclamped(pointB.position, pointC.position, (elapsedTime / totalTime * 0.25f) + 0.1f);

            target.transform.position = move;

            //AdjustTurnRotation(target, transform.position, pointC.position);

            yield return new WaitForSecondsRealtime(0.01f);
            elapsedTime += 0.01f;
        }

        isFinished = true;
        StopCoroutine("TurnMove");
    }

    private void AdjustRotation(Transform target, Vector3 pos, Vector3 posmaisDpos)
    {
        Vector3 direction = (posmaisDpos - pos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        target.rotation = Quaternion.Euler(0, 0, angle - 90f); 
    }

    private void AdjustTurnRotation(Transform target, Vector3 pos, Vector3 posmaisDpos)
    {
        Vector3 direction = (posmaisDpos - pos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        target.rotation = Quaternion.Euler(0, 0, angle); 
    }

    public void SetEntryPoint(Transform nodeTransform)
    {
        entryPoint = nodeTransform;
    }
}
