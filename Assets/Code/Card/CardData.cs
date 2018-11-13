using UnityEngine;

[System.Serializable]
public class CardData {

    public string textToShow;
    public int goodAnswearPoints;
    public Sprite background;

    public CardData(string textToShow, int goodAnswearPoints, Sprite background)
    {
        this.textToShow = textToShow;
        this.goodAnswearPoints = goodAnswearPoints;
        this.background = background;
    }
}
