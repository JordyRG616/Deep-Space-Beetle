using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private List<GameObject> hand = new List<GameObject>();
    private Slot[] slots = new Slot[3];

    private void Awake()
    {
        slots = FindObjectsOfType<Slot>();
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


}
