using System;
using UnityEngine;

public class CardController : MonoBehaviour
{
    CardView mCardView;
    CardData mCardData;

    public void SetupCard(CardData cardData)
    {
        mCardView = GetComponent<CardView>();
        mCardData = cardData;
        mCardView.SetData(mCardData);
    }

    public void RightAnswear()
    {
    }

    public void LeftAnswear()
    {
    }

    public void PlayAnimationUsingOffSet(Vector2 offset)
    {
        //TODO Card Animation set value offset .x
    }
}