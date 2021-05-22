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
    private ParticleSystem particle;

    private void Awake()
    {
        image = GetComponent<Image>();
        grid = FindObjectOfType<GridManager>();

        particle = GetComponentInChildren<ParticleSystem>();
        particle.Stop(true);
        //input = FindObjectOfType<InputMaster>();

        //InputMaster.Main.OnRightMousePressed += ResetTile;
        
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
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            SelectTile();
        } else if(eventData.button == PointerEventData.InputButton.Right)
        {
            ResetTile();
        }
    }

    private void SelectTile()
    {
        HandManager.Main.SetActiveTile(cardHolder);
        particle.Play(true);
        InputMaster.Main.ResetAngle();
        //clicker.SetTileSample(cardHolder);
        grid.OnTilePlaced += RemoveTile;
    }

    public void RemoveTile(object sender, EventArgs e)
    {
        ReceiveDefaultSlot(defaultSlot);
        DeckManager.Main.DrawnCard();
        grid.OnTilePlaced -= RemoveTile;
        particle.Stop(true);
    }

    public void ResetTile()
    {
        DeckManager.Main.AddToDeck(cardHolder);
        ReceiveDefaultSlot(defaultSlot);
        DeckManager.Main.DrawnCard();
        grid.OnTilePlaced -= RemoveTile;
    }

}
