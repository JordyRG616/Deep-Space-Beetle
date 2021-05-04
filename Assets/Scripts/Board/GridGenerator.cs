using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private GridCell[,] cells;
    [SerializeField] private Vector2Int gridSize;

    private void Awake()
    {
        cells = new GridCell[6,6];
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        int x = 0, y = 0;
        while(x < gridSize.x)
        {
            while(y < gridSize.y)
            {
                float posX = ((9.8f / 6f) * x) + transform.position.x;
                float posY = ((9.8f / 6f) * y) + transform.position.y;

                GridCell cell = new GridCell(posX, posY, x, y);
                cells[x,y] = cell;
                
                y++;
            }
            y = 0;
            x++;
        }
    }

    public (int x, int y) GetCellPositionIndex(float posX, float posY)
    {
        int _x = (int)((posX - transform.position.x) * (6f / 9.8f));
        int _y = (int)((posY - transform.position.y) * (6f / 9.8f));

        return (_x, _y);
    }

    public (float x, float y) GetCellWorldPosition(int indexX, int indexY)
    {
        float _x = ((9.8f / 6f) * indexX) + transform.position.x;
        float _y = ((9.8f / 6f) * indexY) + transform.position.y;

        return (_x, _y);
    }

    public GridCell GetCell(float XWorldPosition, float YWorldPosition)
    {
        int _x = GetCellPositionIndex(XWorldPosition, YWorldPosition).x;
        int _y = GetCellPositionIndex(XWorldPosition, YWorldPosition).y;

        return cells[_x, _y];
    }
}
