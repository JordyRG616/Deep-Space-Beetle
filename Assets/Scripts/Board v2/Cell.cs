using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector2Int indexPosition;
    private Vector3 worldPosition;
    private GridManager grid;
    private GameObject previewTile;
    private float angle;

    private void Awake()
    {
        grid = FindObjectOfType<GridManager>();
        grid.OnTilePlaced += DestroyPreview;

        InputMaster.Main.OnRightMousePressed += RotatePreview;
    }

    public void InitiateProperties(int x, int y, Vector3 position)
    {
        indexPosition.x = x;
        indexPosition.y = y;
        worldPosition = position;
    }

    void OnMouseEnter()
    {
        if(HandManager.Main.activeTile != null)
        {
            InstantiatePreview(HandManager.Main.activeTile);
        }
    }

    void OnMouseExit()
    {
        DestroyPreview(this, EventArgs.Empty);
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

        Vector3 axis = this.transform.position + new Vector3(1.65f / 2, 1.65f / 2, 0);
        tileToSpawn.transform.RotateAround(axis, Vector3.forward, InputMaster.Main.AngleToRotate);

        Destroy(this.gameObject);
        grid.TriggerOnTilePlaced();
        HandManager.Main.SetActiveTile(null);
        return tileToSpawn;
    }

    private void InstantiatePreview(GameObject baseTile)
    {
        Color color = Color.green;
        color.a = 0.3f;

        previewTile = Instantiate((GameObject)Resources.Load("Preview"), this.transform.position, Quaternion.identity);

        Vector3 axis = this.transform.position + new Vector3(1.65f / 2, 1.65f / 2, 0);
        previewTile.transform.RotateAround(axis, Vector3.forward, InputMaster.Main.AngleToRotate);
        
        previewTile.GetComponent<SpriteRenderer>().sprite = baseTile.GetComponent<SpriteRenderer>().sprite;
        previewTile.GetComponent<SpriteRenderer>().color = color;
    }

    public void DestroyPreview(object sender, EventArgs e)
    {
        if(previewTile != null)
        {
            Destroy(previewTile);
        }
    }

    public void RotatePreview(object sender, EventArgs e)
    {
        Debug.Log(InputMaster.Main.AngleToRotate);
        if(previewTile != null)
        {
            Vector3 axis = this.transform.position + new Vector3(1.65f / 2, 1.65f / 2, 0);
            previewTile.transform.RotateAround(axis, Vector3.forward, 90f);
        }
    }

    void OnDestroy()
    {
        grid.OnTilePlaced -= DestroyPreview;
    }
}
