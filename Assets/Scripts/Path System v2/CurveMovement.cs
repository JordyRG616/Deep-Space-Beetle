using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : MovementTemplate
{
    public override bool isFinished { get; set; }
    public override float elapsedTime { get; set; }
    public override float totalTime { get; set; }

    public override IEnumerator CalculateMovement(Transform target, Transform[] points)
    {
        isFinished = false;
        while(elapsedTime <= totalTime)
        {
            Vector3 movingPointA = Vector3.Lerp(points[0].position, points[1].position, elapsedTime / totalTime);
            Vector3 MovingPointB = Vector3.Lerp(points[1].position, points[2].position, elapsedTime / totalTime);

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

    public override void DoMovement(Transform target, NodeComponent[] nodes, float time)
    {
        totalTime = time;
        StartCoroutine(CalculateMovement(target, NodesToTransfoms(nodes)));
    }
}
