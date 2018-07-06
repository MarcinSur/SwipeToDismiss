using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour {
    public Image cardImage;
    public Image topImage;
    public Text cardName;

    public void SetCard(CardData cardData)
    {
        //TO DO set card
        cardName.text = cardData.textToShow;
    }
}
