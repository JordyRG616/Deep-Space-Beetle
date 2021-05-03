using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] private BeetleMovement beetle;
    [SerializeField] private GameObject startPath;
    private GameObject nextPath;
    private List<GameObject> allNodes = new List<GameObject>();
    [SerializeField] private float linkThreshold;

    private void Awake()
    {
        //allNodes = GameObject.FindGameObjectsWithTag("Node").ToList();
        beetle.SetActivePath(startPath);
    }

    private void Update()
    {
        CheckNextPath();
    }

    private void CheckNextPath()
    {
        if(beetle.GetTimes().elapsedTime >= beetle.GetTimes().totalTime)
        {
            Collider2D nextNode = Physics2D.OverlapCircle(beetle.transform.position, .3f);
            if(nextNode != null)
            {
                Debug.Log(nextNode.gameObject.name);
                beetle.SetActivePath(nextNode.transform.parent.gameObject);
                Destroy(nextNode);
            }
        }
    }
}
