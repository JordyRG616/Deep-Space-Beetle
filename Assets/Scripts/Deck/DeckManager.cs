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

    private void Shuffle()
    {
        while(baseDeck.Count > 0)
        {
            int rdm = Random.Range(0, baseDeck.Count);
            GameObject cardContainer = baseDeck[rdm];

            deck.Enqueue(cardContainer);
            baseDeck.Remove(cardContainer);
        }
    }

    private void DrawnCard()
    {
        if(deck.Count > 0)
        {
            hand.AddCard(deck.Dequeue());
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            DrawnCard();
        }
    }
}
