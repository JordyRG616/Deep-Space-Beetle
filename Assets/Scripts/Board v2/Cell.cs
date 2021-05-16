using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector2Int indexPosition;
    private Vector3 worldPosition;
    private GridManager grid;

    private void Awake()
    {
        grid = FindObjectOfType<GridManager>();
    }

    public void InitiateProperties(int x, int y, Vector3 position)
    {
        indexPosition.x = x;
        indexPosition.y = y;
        worldPosition = position;
    }

    void OnMouseOver()
    {
        
    }

    void OnMouseDown()
    {
        if(HandManager.Main.activeTile != null)
        {
            InstantiateTile(HandManager.Main.activeTile);
        }
    }

    public GameObject InstantiateTile(GameObject tile)
    {
        GameObject tileToSpawn = Instantiate(tile, this.transform.position, Quaternion.identity, this.transform.parent);
        Destroy(this.gameObject);
        grid.TriggerOnTilePlaced();
        return tileToSpawn;
    }
}
