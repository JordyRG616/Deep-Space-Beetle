using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMovement : MovementTemplate
{
    public override bool isFinished { get ; set ; }
    public override float elapsedTime { get ; set ; }
    public override float totalTime { get ; set ; }

    public override IEnumerator CalculateMovement(Transform target, Transform[] points)
    {
        isFinished = false;

        AdjustRotation(target, points[0].position, points[1].position);

        while(elapsedTime <= totalTime * 0.75f)
        {
            Vector3 move = Vector3.Lerp(points[0].position, points[1].position, elapsedTime / (totalTime * 0.75f));
            Vector3 nextPoint = Vector3.LerpUnclamped(points[0].position, points[1].position, (elapsedTime / totalTime * 0.75f) + 0.1f);

            target.transform.position = move;

            //AdjustTurnRotation(target, transform.position, points[1].position);

            yield return new WaitForSecondsRealtime(0.01f);
            elapsedTime += 0.01f;
        }

        AdjustRotation(target, points[1].position, points[2].position);
        elapsedTime = 0f;

        while(elapsedTime <= totalTime *0.25f)
        {
            Vector3 move = Vector3.Lerp(points[1].position, points[2].position, elapsedTime / (totalTime * 0.25f));
            Vector3 nextPoint = Vector3.LerpUnclamped(points[1].position, points[2].position, (elapsedTime / totalTime * 0.25f) + 0.1f);

            target.transform.position = move;

            //AdjustTurnRotation(target, transform.position, points[2].position);

            yield return new WaitForSecondsRealtime(0.01f);
            elapsedTime += 0.01f;
        }

        isFinished = true;
        elapsedTime = 0f;
        StopCoroutine("TurnMove");
    }

    public override void DoMovement(Transform target, NodeComponent[] nodes, float time)
    {
        totalTime = time;
        StartCoroutine(CalculateMovement(target, NodesToTransfoms(nodes)));
    }
}
