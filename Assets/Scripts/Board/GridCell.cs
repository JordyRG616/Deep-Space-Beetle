
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    private Vector2 worldPosition = new Vector2();
    private Vector2Int positionIndex = new Vector2Int();
    private bool hasTile;

    public GridCell(float sizeX, float sizeY, int posX, int posY)
    {
        worldPosition = new Vector2(sizeX, sizeY);
        positionIndex = new Vector2Int(posX, posY);
    }

    public (int x, int y) GetPositionIndex()
    {
        return(positionIndex.x, positionIndex.y);
    }

    public (float x, float y) GetWorldPosition()
    {
        return(worldPosition.x, worldPosition.y);
    }

    public void SetHasTile(bool state)
    {
        hasTile = state;
    }

    public bool GetHasTile()
    {
        return hasTile;
    }
}
