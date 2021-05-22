using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementTemplate : MonoBehaviour
{
    public abstract bool isFinished {get; set;}
    public abstract float elapsedTime {get; set;}
    public abstract float totalTime {get; set;}

    public abstract void DoMovement(Transform target, NodeComponent[] nodes, float time);

    public abstract IEnumerator CalculateMovement(Transform target, Transform[] points);

    internal void AdjustRotation(Transform target, Vector3 pos, Vector3 posmaisDpos)
    {
        Vector3 direction = (posmaisDpos - pos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        target.rotation = Quaternion.Euler(0, 0, angle - 90f); 
    }

    internal Transform[] NodesToTransfoms(NodeComponent[] nodes)
    {
        List<Transform> transforms = new List<Transform>();
        foreach(NodeComponent node in nodes)
        {
            transforms.Add(node.transform);
        }

        return transforms.ToArray();
    }
}
