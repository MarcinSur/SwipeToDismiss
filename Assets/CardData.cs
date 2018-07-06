using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData {

    public string textToShow;
    public int goodAnswearPoints;

    public CardData(string textToShow, int goodAnswearPoints)
    {
        this.textToShow = textToShow;
        this.goodAnswearPoints = goodAnswearPoints;
    }
}
