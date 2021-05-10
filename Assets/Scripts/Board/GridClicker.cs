using System;
using System.Collections.Generic;
using UnityEngine;

public class GridClicker : MonoBehaviour
{
    GridGenerator grid;
    GameObject tileSample;
    private InputMaster input;

    public event EventHandler OnTilePlaced;



    private void Awake()
    {
        input = FindObjectOfType<InputMaster>();
        input.OnLeftMousePressed += PlaceTile;

        grid = GetComponent<GridGenerator>();
    }

    public void PlaceTile(object sender, EventArgs e)
    {
        if(NeighbourHasTile())
        {
            InstantiateNewTile(ReturnWorldPosition().x, ReturnWorldPosition().y);
            OnTilePlaced?.Invoke(this, TileEventArgs.Empty);
        }
    }

    /*private void Update()
    {
        
        if(Input.GetMouseButtonDown(0) && NeighbourHasTile())
        {
            InstantiateNewTile(ReturnWorldPosition().x, ReturnWorldPosition().y);
        }
    }*/

    private (float x, float y) ReturnWorldPosition()
    {
        float posX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float posY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        return (grid.GetCellWorldPosition
        (
            grid.GetCellPositionIndex(posX, posY).x, 
            grid.GetCellPositionIndex(posX, posY).y 
        ));
    }

    private void InstantiateNewTile(float x, float y)
    {
        float Xposition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float Yposition = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if(ClickedOnGrid() && !grid.GetCell(Xposition, Yposition).GetHasTile() && tileSample != null)
        {
            Vector3 tilePosition = new Vector3 (x, y, 0);
            GameObject tile = Instantiate(tileSample, tilePosition, Quaternion.identity);

            grid.GetCell(Xposition, Yposition).SetHasTile(true);
        }
    }

    private bool ClickedOnGrid()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(mousePosition.x < transform.position.x || mousePosition.x > transform.position.x + 9.8)
        {
            return false;
        }

        else if(mousePosition.y < transform.position.y || mousePosition.y > transform.position.y + 9.8)
        {
            return false;
        }

        else
        {
            return true;
        }
    }

    public void SetTileSample(GameObject tile)
    {
        tileSample = tile;
    }

    private List<GridCell> GetNeighbouringCells(float x, float y)
    {
        List<GridCell> neighbours = new List<GridCell>();
        int ogXIndex = grid.GetCellPositionIndex(x, y).x;
        int ogYIndex = grid.GetCellPositionIndex(x, y).y;

        for (int _x = -1; _x <= 1; _x++)
        {
            for (int _y = -1; _y <= 1; _y++)
            {
                if(_x != _y && _x != -_y)
                {
                    if(ogXIndex + _x >= 0 && ogYIndex + _y >= 0)
                    {
                        if(ogXIndex + _x <= 5 && ogYIndex + _y <= 5)
                        {
                            GridCell cell = grid.GetCell(ogXIndex + _x, ogYIndex + _y);
                            neighbours.Add(cell);
                        }
                    }
                }
            }
        }

        return neighbours;
    }

    private bool NeighbourHasTile()
    {
        float posX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float posY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        foreach(GridCell cell in GetNeighbouringCells(posX, posY))
        {
            if(cell.GetHasTile())
            {
                return true;
            }
        }

        return false;
    }

    void OnDisable()
    {
        input.OnLeftMousePressed -= PlaceTile;
    }
}

public class TileEventArgs : EventArgs
{

}
