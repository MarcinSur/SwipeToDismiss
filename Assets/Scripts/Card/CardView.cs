using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour {
    public SpriteRenderer background;
    public Image topImage;
    public Text title;

    public void SetData(CardData cardData)
    {
        title.text = cardData.textToShow;
        background.sprite = cardData.background;
    }
}
