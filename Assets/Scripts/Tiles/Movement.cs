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

    public void DoMovement(Transform target, float time)
    {
        totalTime = time;

        switch(type)
        {
            case MovementType.Straight:
                if(entryPoint = nodes[0])
                {
                    StartCoroutine(StraightMove(target, nodes[0], nodes[1]));
                } else
                  {
                    StartCoroutine(StraightMove(target, nodes[1], nodes[0]));
                  }
                break;
            case MovementType.Curve:
                if(entryPoint = nodes[0])
                {
                StartCoroutine(CurveMove(target, nodes[0], nodes[1], nodes[2]));
                } else
                    {
                StartCoroutine(CurveMove(target, nodes[2], nodes[1], nodes[0]));
                    }
                break;
            case MovementType.Turn:
                if(entryPoint = nodes[0])
                {
                StartCoroutine(TurnMove(target, nodes[0], nodes[1], nodes[2]));
                } else
                    {
                StartCoroutine(TurnMove(target, nodes[2], nodes[1], nodes[0]));
                    }
                break;
        }
    }

    private IEnumerator StraightMove(Transform target, Transform pointA, Transform pointB)
    {
        isFinished = false;
        while(elapsedTime <= totalTime)
        {
            Vector3 move = Vector3.Lerp(pointA.position, pointB.position, elapsedTime / totalTime);
            Vector3 nextPoint = Vector3.LerpUnclamped(pointA.position, pointB.position, (elapsedTime / totalTime) + 0.1f);

            target.transform.position = move;
            
            AdjustRotation(target, transform.position, nextPoint);
            
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
        while(elapsedTime <= totalTime * 0.75f)
        {
            Vector3 move = Vector3.Lerp(pointA.position, pointB.position, elapsedTime / (totalTime * 0.75f));
            Vector3 nextPoint = Vector3.LerpUnclamped(pointA.position, pointB.position, (elapsedTime / totalTime * 0.75f) + 0.1f);

            target.transform.position = move;

            AdjustRotation(target, transform.position, nextPoint);

            yield return new WaitForSecondsRealtime(0.01f);
            elapsedTime += 0.01f;
        }

        elapsedTime = 0f;

        while(elapsedTime <= totalTime *0.25f)
        {
            Vector3 move = Vector3.Lerp(pointB.position, pointC.position, elapsedTime / (totalTime * 0.25f));
            Vector3 nextPoint = Vector3.LerpUnclamped(pointB.position, pointC.position, (elapsedTime / totalTime * 0.25f) + 0.1f);

            target.transform.position = move;

            AdjustRotation(target, transform.position, nextPoint);

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

    public void SetEntryPoint(Transform nodeTransform)
    {
        entryPoint = nodeTransform;
    }
}
