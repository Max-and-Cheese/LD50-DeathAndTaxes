using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public Button cardButton;

    public TMP_Text cardTitle;
    public TMP_Text cardDescription;
    public Image cardImage;

    public CardData data;
    // Start is called before the first frame update
    void Start() {
        if (data) {
            cardTitle.text = data.cardName;
            cardDescription.text = data.cardDescription;
            cardImage.sprite = data.cardIcon;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
