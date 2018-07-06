using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardController : MonoBehaviour {

    public CardData cardData;
    public CardView cardView;
    public Drag dragBehaviour;

    public void SetCard(CardData cardData)
    {
        this.cardData = cardData;
        cardView.SetCard(cardData);
    }

    public void MoveToRightWithDownAnimation()
    {
        float moveTo = Drag.screenWidth * 2;

        dragBehaviour.rectTransformToDrag.DORotate(new Vector3(0, 0, -20), .8f);
        dragBehaviour.rectTransformToDrag.DOMove(new Vector3(moveTo, dragBehaviour.rectTransformToDrag.position.y - Drag.screenWidth / 10), 1f).OnComplete(() => { Destroy(gameObject); });

    }

    public void MoveToLeftWithDownAnimation()
    {
        float moveTo = -Drag.screenWidth;
        Debug.Log("CARD MoveToLeftWithDownAnimation");

        dragBehaviour.rectTransformToDrag.DORotate(new Vector3(0, 0, 20), .8f);
        dragBehaviour.rectTransformToDrag.DOMove(new Vector3(moveTo, dragBehaviour.rectTransformToDrag.position.y - -Drag.screenHeight / 10), 1f).OnComplete(() => { Destroy(gameObject); });
    }

    public void BackToCenter()
    {
        dragBehaviour.rectTransformToDrag.DOLocalMove(new Vector3(0, 0, 0), .5f);
        dragBehaviour.rectTransformToDrag.DOLocalRotate(new Vector3(0, 0, 0), .5f);
    }

    public void TopBarAnimation()
    {
    }
}
