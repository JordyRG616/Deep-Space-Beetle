using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject emptyCell;
    [SerializeField] private int Xsize, Ysize;
    [SerializeField] private float cellSize;
    [SerializeField] private GameObject beetle;
    private Cell[,] cells = new Cell[100,100];
    public GameObject InitialTile { get; private set;}
    

    public event EventHandler OnTilePlaced;

    private void Awake()
    {
        GenerateGrid();
    }

    public void SetInitialTile()
    {
        int x = UnityEngine.Random.Range(1, Xsize - 1);
        int y = UnityEngine.Random.Range(1, Ysize - 1);

        InitialTile = GetCell(x, y).InstantiateTile(DeckManager.Main.GetCard());
    }

    public void InstantiateShip()
    {
        MovementProcessor initialMovement = InitialTile.GetComponentInChildren<MovementProcessor>();
        NodeComponent[] nodes = initialMovement.GetComponentsInChildren<NodeComponent>();
        /*GameObject initialNode = nodes[0].gameObject;
        Destroy(nodes[0]);
        nodes[0] = initialNode.AddComponent<InitialNode>();
        initialNode.tag = "Untagged";*/
        nodes[0].isInitialNode = true;
        nodes[0].gameObject.tag = "InitialNode";
        initialMovement.SetNodeList(nodes[0], nodes[1], nodes[2]);

        BeetleShip ship = Instantiate(beetle, this.transform.position, Quaternion.identity).GetComponent<BeetleShip>();

        foreach(NodeComponent node in nodes)
        {
            node.isActiveNode = true;
            node.SetBeetleShip(ship);
        }

        ship.AddToQueue(initialMovement);

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
