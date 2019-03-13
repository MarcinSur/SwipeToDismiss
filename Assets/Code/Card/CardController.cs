using System;
using UnityEngine;
using DG.Tweening;

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

    public void PlayAnimationBackToCenter()
    {
    }

    public void PlayAnimationUsingOffSet(Vector2 movingVector)
    {
        transform.DORotate(new Vector3(0, 0, -movingVector.x), 0);
        transform.DOLocalMoveX(movingVector.x, 0);
    }
}