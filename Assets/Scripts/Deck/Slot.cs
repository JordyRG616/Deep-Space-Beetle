using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    private bool ocuppied;
    private GameObject cardHolder;

    private void Awake()
    {
        image = GetComponent<Image>();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(cardHolder.name);
    }
}
