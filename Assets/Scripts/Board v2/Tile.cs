using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GridManager grid;

    private void Awake()
    {
        grid = FindObjectOfType<GridManager>();
    }

    public GameObject InstantiateTile(GameObject tile)
    {
        GameObject tileToSpawn = Instantiate(tile, this.transform.position, Quaternion.identity, this.transform.parent);
        Destroy(this.gameObject);
        grid.TriggerOnTilePlaced();
        return tileToSpawn;
    }
    
    void OnMouseDown()
    {
        if(HandManager.Main.activeTile != null)
        {
            InstantiateTile(HandManager.Main.activeTile);
        }
    }
}
