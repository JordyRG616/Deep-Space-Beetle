using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    private bool ocuppied;
    private GameObject cardHolder;
    private GridClicker clicker;
    private GameObject defaultSlot;
    private DeckManager deck;
    private InputMaster input;

    private void Awake()
    {
        image = GetComponent<Image>();
        clicker = FindObjectOfType<GridClicker>();
        deck = FindObjectOfType<DeckManager>();
        input = FindObjectOfType<InputMaster>();

        input.OnRightMousePressed += ResetTile;
        
    }

    public bool HasCard()
    {
        return ocuppied;
    }

    public void ReceiveCard(GameObject card)
    {
        cardHolder = card;
        image.sprite = card.GetComponent<SpriteRenderer>().sprite;
        image.enabled = true;
        ocuppied = true;
    }

    public void ReceiveDefaultSlot(GameObject defaultSlot)
    {
        cardHolder = defaultSlot;
        image.enabled = false;
        ocuppied = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clicker.SetTileSample(cardHolder);
        clicker.OnTilePlaced += RemoveTile;
    }

    public void RemoveTile(object sender, EventArgs e)
    {
        ReceiveDefaultSlot(defaultSlot);
        deck.DrawnCard();
        clicker.OnTilePlaced -= RemoveTile;
    }

    public void ResetTile(object sender, EventArgs e)
    {
        deck.AddToDeck(cardHolder);
        ReceiveDefaultSlot(defaultSlot);
        deck.DrawnCard();
        clicker.OnTilePlaced -= RemoveTile;
    }

}
