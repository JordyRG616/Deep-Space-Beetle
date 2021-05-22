using System;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{

    #region SINGLETON
    private static HandManager _instance;
    public static HandManager Main
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<HandManager>();

                if(_instance == null)
                {
                    GameObject container = new GameObject("Action Archive");
                    _instance = container.AddComponent<HandManager>();
                }
            }              

            return _instance;
        } 
    }
    #endregion

    private List<GameObject> hand = new List<GameObject>();
    public GameObject activeTile {get; private set;}
    private Slot[] slots = new Slot[3];

    private void Awake()
    {
        slots = FindObjectsOfType<Slot>();

        InputMaster.Main.OnRightMousePressed += RotateTile;        
    }

    public void AddCard(GameObject card)
    {
        foreach(Slot slot in slots)
        {
            if(!slot.HasCard())
            {
                hand.Add(card);
                slot.ReceiveCard(card);
                break;
            }
        }
    }

    public Slot[] GetSlots()
    {
        return slots;
    }

    public void SetActiveTile(GameObject tile)
    {
        activeTile = tile;
    }

    public void RotateTile(object sender, EventArgs e)
    {
        if(activeTile != null)
        {
            activeTile.transform.Rotate(Vector3.forward, 90f, Space.World);
        }
    }
}
