using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    public float speed;   
}




/*{
    private float elapsedTime;
    [SerializeField] private float totalTime = 1.5f;
    private List<Transform> nodes = new List<Transform>();
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void SetNodes(GameObject path)
    {
        nodes.Clear();
        nodes = path.GetComponentsInChildren<Transform>().ToList();
        nodes.Remove(path.transform);

        //transform.position = nodes[0].position;
        elapsedTime = 0f;
        StartCoroutine(LerpMove(nodes[0], nodes[1], nodes[2]));
    }

    private IEnumerator LerpMove(Transform node1, Transform node2, Transform node3)
    {
        while(elapsedTime <= totalTime)
        {
            Vector3 lerpNodes12 = Vector3.Lerp(node1.position, node2.position, (elapsedTime) / totalTime);
            Vector3 lerpNodes23 = Vector3.Lerp(node2.position, node3.position, (elapsedTime) / totalTime);

            Vector3 lerpTotal = Vector3.Lerp(lerpNodes12, lerpNodes23, (elapsedTime) / totalTime);
            Vector3 nextPoint = Vector3.LerpUnclamped(lerpNodes12, lerpNodes23, ((elapsedTime) / totalTime) + 0.1f);

            transform.position = lerpTotal;

            AdjustRotation(transform.position, nextPoint);

            yield return new WaitForSecondsRealtime(0.01f);
            elapsedTime += 0.01f;
        }

        //Destroy(gameObject);
    }

    private void AdjustRotation(Vector3 pos, Vector3 posmaisDpos)
    {
        Vector3 direction = (posmaisDpos - pos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f); 
    }

    public void SetActivePath(GameObject newPath)
    {
        SetNodes(newPath);
    }

    public (float elapsedTime, float totalTime) GetTimes()
    {
        return (elapsedTime, totalTime);
    }

    public GameObject GetLastNode()
    {
        return nodes[2].gameObject;
    }
}*/
