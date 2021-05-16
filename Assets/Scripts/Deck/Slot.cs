using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    private bool ocuppied;
    private GameObject cardHolder;
    private GridManager grid;
    private GameObject defaultSlot;
    private InputMaster input;

    private void Awake()
    {
        image = GetComponent<Image>();
        grid = FindObjectOfType<GridManager>();
        //input = FindObjectOfType<InputMaster>();

        InputMaster.Main.OnRightMousePressed += ResetTile;
        
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
        HandManager.Main.SetActiveTile(cardHolder);

        //clicker.SetTileSample(cardHolder);
        grid.OnTilePlaced += RemoveTile;
    }

    public void RemoveTile(object sender, EventArgs e)
    {
        ReceiveDefaultSlot(defaultSlot);
        DeckManager.Main.DrawnCard();
        grid.OnTilePlaced -= RemoveTile;
    }

    public void ResetTile(object sender, EventArgs e)
    {
        DeckManager.Main.AddToDeck(cardHolder);
        ReceiveDefaultSlot(defaultSlot);
        DeckManager.Main.DrawnCard();
        grid.OnTilePlaced -= RemoveTile;
    }

}
