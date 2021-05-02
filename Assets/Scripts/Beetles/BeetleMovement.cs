using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleMovement : MonoBehaviour
{
    [SerializeField] private GameObject path;
    [SerializeField][Range(0, 1f)] private float elapsedTime;
    private List<Transform> nodes = new List<Transform>();

    private void Awake()
    {
        nodes = path.GetComponentsInChildren<Transform>().ToList();
        nodes.Remove(path.transform);

        transform.position = nodes[0].position;
    }

    private void LerpMove(Transform node1, Transform node2, Transform node3)
    {
        Vector3 lerpNodes12 = Vector3.Lerp(node1.position, node2.position, elapsedTime);
        Vector3 lerpNodes23 = Vector3.Lerp(node2.position, node3.position, elapsedTime);

        Vector3 lerpTotal = Vector3.Lerp(lerpNodes12, lerpNodes23, elapsedTime);
        Vector3 nextlerp = Vector3.LerpUnclamped(lerpNodes12, lerpNodes23, elapsedTime + 0.1f);

        transform.position = lerpTotal;

        AdjustRotation(transform.position, nextlerp);
    }

    private void AdjustRotation(Vector3 pos, Vector3 posmaisDpos)
    {
        Vector3 direction = (posmaisDpos - pos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f); 
    }

    private void Update()
    {
        LerpMove(nodes[0], nodes[1], nodes[2]);
    }
}
