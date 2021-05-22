using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMovement : MovementTemplate
{
    public override bool isFinished { get ; set ; }
    public override float elapsedTime { get ; set ; }
    public override float totalTime { get ; set ; }

    public override IEnumerator CalculateMovement(Transform target, Transform[] points)
    {
        isFinished = false;

        AdjustRotation(target, points[0].position, points[2].position);


        while(elapsedTime <= totalTime)
        {
            Vector3 move = Vector3.Lerp(points[0].position, points[2].position, elapsedTime / totalTime);

            target.transform.position = move;
            
            yield return new WaitForSecondsRealtime(0.01f);
            elapsedTime += 0.01f;
        }

        isFinished = true;
        elapsedTime = 0f;
        StopCoroutine("StraightMove");
    }

    public override void DoMovement(Transform target, NodeComponent[] nodes, float time)
    {
        totalTime = time;
        StartCoroutine(CalculateMovement(target, NodesToTransfoms(nodes)));
    }
}
