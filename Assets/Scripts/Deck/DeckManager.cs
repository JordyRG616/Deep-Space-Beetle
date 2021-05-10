using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<GameObject> baseDeck = new List<GameObject>();
    [SerializeField] private int handSize;
    private Queue<GameObject> deck = new Queue<GameObject>();
    private HandManager hand;


    private void Awake()
    {
        Shuffle();
        hand = FindObjectOfType<HandManager>();
    }

    void Start()
    {
        for (int i = 0; i <+ handSize; i++)
        {
            DrawnCard();
        }
    }

    private void Shuffle()
    {
        while(baseDeck.Count > 0)
        {
            int rdm = UnityEngine.Random.Range(0, baseDeck.Count);
            GameObject cardContainer = baseDeck[rdm];

            deck.Enqueue(cardContainer);
            baseDeck.Remove(cardContainer);
        }
    }

    public void DrawnCard()
    {
        if(deck.Count > 0)
        {
            hand.AddCard(deck.Dequeue());
        }
    }

    public void AddToDeck(GameObject item)
    {
        deck.Enqueue(item);
        Shuffle();
        Debug.Log(deck.Count);
    }

}
