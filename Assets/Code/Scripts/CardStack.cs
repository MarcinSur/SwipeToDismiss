using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CardStack : MonoBehaviour
{
    public IDragSwipe swipeSystem = new TestSwipe();
    public GameObject prefab;
    public List<CardData> dataCards = new List<CardData>();
    public List<CardController> controllerCards = new List<CardController>();
    public CardRepository cardRepository;
    private GameObject mCurrentStackElement;
    private CardController mCurrentCardController;

    private void OnEnable()
    {
        swipeSystem = gameObject.GetComponent<TestSwipe>();
        swipeSystem.SwipeLeftEvent += MovedLeft;
        swipeSystem.SwipeRightEvent += MovedRight;
        swipeSystem.SwipeCanceledEvent += BackToCenter;
    }

    private void OnDisable()
    {
        swipeSystem.SwipeLeftEvent -= MovedLeft;
        swipeSystem.SwipeRightEvent -= MovedRight;
        swipeSystem.DragEvent -= MovingCard;
        swipeSystem.SwipeCanceledEvent -= BackToCenter;
    }

    private void Start()
    {
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
        swipeSystem.DragEvent += MovingCard;

        //swipeSystem.enableSwipe = true;
    }

    public void DestroyCard(GameObject card)
    {
        mCurrentStackElement = null;
        mCurrentCardController = null;
        Destroy(mCurrentStackElement);
    }

    public void MovingCard(object sender, DragArgs e)
    {
       mCurrentCardController.PlayAnimationUsingOffSet(e.drag);

    }

    public void MovedRight(object sender, EventArgs e)
    {
        Debug.Log("CARD RIGHT");
    }

    public void MovedLeft(object sender, EventArgs e)
    {
        Debug.Log("CARD Left distance  ");
    }

    public void BackToCenter(object sender, EventArgs e)
    {
        Debug.Log("Center  ");
    }
}

