using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardStack : MonoBehaviour
{
    public Drag dragSystem;
    public RectTransform parentCardPrefab;
    public GameObject cardPrefab;
    public List<CardData> dataCards = new List<CardData>();
    public List<CardController> controllerCards = new List<CardController>();
    public CardRepository cardRepository;

    private GameObject currentCard;

    private void Start()
    {
        LoadCards();
    }

    public void LoadCards()
    {
        dataCards = cardRepository.tempDataCard;
    }

    public void  SetCardStack(List<CardData> cards)
    {
        this.dataCards = cards;
    }

    public void InstantiateCard()
    {
        if (dataCards.Count == 0)
            return;

        currentCard = Instantiate(cardPrefab, parentCardPrefab);
        currentCard.GetComponent<CardController>().SetCard(dataCards[0]);
        currentCard.GetComponent<Drag>().onRightCompleted.AddListener(cardRight);
        currentCard.GetComponent<Drag>().onLeftCompleted.AddListener(cardLeft);
        dragSystem.Init(currentCard.GetComponent<RectTransform>());
    }

    public void DestroyCard()
    {
        currentCard.GetComponent<Drag>().onRightCompleted.RemoveListener(cardRight);
        currentCard.GetComponent<Drag>().onLeftCompleted.RemoveListener(cardLeft);
        //Destroy(currentCard);
    }

    private void cardRight()
    {
        Debug.Log("CARD RIGHT");
        DestroyCard();
    }

    private void cardLeft()
    {
        Debug.Log("CARD Left");
        DestroyCard();
    }
}
