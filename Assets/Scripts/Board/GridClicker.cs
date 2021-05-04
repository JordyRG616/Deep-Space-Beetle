using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClicker : MonoBehaviour
{
    GridGenerator grid;
    [SerializeField] GameObject tileSample;

    private void Awake()
    {
        grid = GetComponent<GridGenerator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            InstantiateNewTile(ReturnWorldPosition().x, ReturnWorldPosition().y);
        }
    }

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
        if(ClickedOnGrid() && !grid.GetCell(x, y).GetHasTile())
        {
            Vector3 tilePosition = new Vector3 (x, y, 0);
            GameObject tile = Instantiate(tileSample, tilePosition, Quaternion.identity);

            grid.GetCell(x, y).SetHasTile(true);
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
}
