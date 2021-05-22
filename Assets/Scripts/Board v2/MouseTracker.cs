using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    private GridManager grid;

    private void Update()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(-1.65f / 2, -1.65f / 2, 10);
    }

    private void Awake()
    {
        grid = FindObjectOfType<GridManager>();
        grid.OnTilePlaced += (object sender, EventArgs e) => Destroy(this.gameObject);
    }
}
