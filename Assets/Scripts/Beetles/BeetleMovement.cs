using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleMovement : MonoBehaviour
{
    [SerializeField] private GameObject path;
    private float elapsedTime;
    [SerializeField] private float totalTime = 1.5f;
    private List<Transform> nodes = new List<Transform>();

    private void Awake()
    {
        nodes = path.GetComponentsInChildren<Transform>().ToList();
        nodes.Remove(path.transform);

        transform.position = nodes[0].position;
    }
    
    private void Start()
    {
        StartCoroutine(LerpMove(nodes[0], nodes[1], nodes[2]));
    }

    private IEnumerator LerpMove(Transform node1, Transform node2, Transform node3)
    {
        float velocity = CalculateVelocity(node1.position, node3.position);

        while(elapsedTime < totalTime)
        {
            Vector3 lerpNodes12 = Vector3.Lerp(node1.position, node2.position, elapsedTime * velocity);
            Vector3 lerpNodes23 = Vector3.Lerp(node2.position, node3.position, elapsedTime * velocity);

            Vector3 lerpTotal = Vector3.Lerp(lerpNodes12, lerpNodes23, elapsedTime * velocity);
            Vector3 nextlerp = Vector3.LerpUnclamped(lerpNodes12, lerpNodes23, (elapsedTime *velocity) + 0.1f);

            transform.position = lerpTotal;

            AdjustRotation(transform.position, nextlerp);

            yield return new WaitForSecondsRealtime(0.1f);
            elapsedTime += 0.1f;
        }

            elapsedTime = 0f;
    }

    private float CalculateVelocity(Vector3 startPoint, Vector3 endPoint)
    {
        float distance = (startPoint - endPoint).magnitude;
        return distance / totalTime;
    }

    private void AdjustRotation(Vector3 pos, Vector3 posmaisDpos)
    {
        Vector3 direction = (posmaisDpos - pos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f); 
    }

    //private void Update()
    //{
    //    LerpMove(nodes[0], nodes[1], nodes[2]);
    //}
}
