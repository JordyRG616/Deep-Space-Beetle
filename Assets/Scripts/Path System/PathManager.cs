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
        allNodes = GameObject.FindGameObjectsWithTag("Node").ToList();
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
            allNodes.Remove(beetle.GetLastNode());
            foreach(GameObject node in allNodes)
            {
                float distance = (beetle.GetLastNode().transform.position - node.transform.position).magnitude;

                if(distance <= linkThreshold)
                {
                    nextPath = node.transform.parent.gameObject;
                    beetle.SetActivePath(nextPath);
                }
            }
        }
    }
}
