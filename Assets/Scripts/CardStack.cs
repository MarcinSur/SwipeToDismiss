using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CardStack : MonoBehaviour
{
    public Swipe swipeSystem;
    public GameObject prefab;
    public List<CardData> dataCards = new List<CardData>();
    public List<CardController> controllerCards = new List<CardController>();
    public CardRepository cardRepository;
    private GameObject mCurrentStackElement;
    private CardController mCurrentCardController;

    public CardStack()
    {

    }

    private void Start()
    {
        swipeSystem = gameObject.GetComponent<Swipe>();
        swipeSystem.swipedLeft.AddListener(MovedLeft);
        swipeSystem.swipedRight.AddListener(MovedRight);
        swipeSystem.drag.AddListener(Drag);
        LoadCards();
    }

    public void LoadCards()
    {
        dataCards = cardRepository.tempDataCard;
    }

    public void SetCardStack(List<CardData> cards)
    {
        this.dataCards = cards;
    }

    public void InstantiateCard()
    {
        if (dataCards.Count == 0)
            return;

        mCurrentStackElement = Instantiate(prefab);
        mCurrentCardController = mCurrentStackElement.GetComponent<CardController>();
        
        mCurrentCardController.SetupCard(dataCards[0]);
        swipeSystem.enableSwipe = true;
    }

    public void DestroyCard(GameObject card)
    {
        mCurrentStackElement = null;
        mCurrentCardController = null;
        Destroy(mCurrentStackElement);
    }

    public void Drag()
    {
       // mCurrentCardController.PlayAnimationUsingOffSet(Swipe.offSet);
    }

    public void MovedRight()
    {
        Debug.Log("CARD RIGHT");
        mCurrentCardController.RightAnswear();
    }

    public void MovedLeft()
    {
        Debug.Log("CARD Left");
        mCurrentCardController.LeftAnswear();
    }
}

