using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject emptyCell;
    private Cell[,] cells = new Cell[100,100];
    [SerializeField] private int Xsize, Ysize;
    [SerializeField] private float cellSize;
    public GameObject InitialTile { get; private set;}

    public event EventHandler OnTilePlaced;

    private void Awake()
    {
        GenerateGrid();
        int x = UnityEngine.Random.Range(1, Xsize - 1);
        int y = UnityEngine.Random.Range(1, Ysize - 1);

        InitialTile = GetCell(x, y).InstantiateTile(DeckManager.Main.GetCard());

    }

    void Start()
    {
        FindObjectOfType<BeetlesManager>().StartMovement();
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < Xsize; x++)
        {
            for (int y = 0; y < Ysize; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0) + this.transform.position;
                GameObject cellContainer = Instantiate(emptyCell, position, Quaternion.identity, this.transform);
                Cell cell = cellContainer.GetComponent<Cell>();
                cell.InitiateProperties(x, y, position);
                cells[x,y] = cell;
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        return cells[x, y];
    }

    public void TriggerOnTilePlaced()
    {
        OnTilePlaced?.Invoke(this, EventArgs.Empty);
    }
}
