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
            data.Setup();
            cardTitle.text = data.cardName;
            cardDescription.text = data.GetFrontDescriptions();
            cardImage.sprite = data.cardIcon;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnClicked() {
        if (GameManager.Instance.GAME_OVER) return;

        if (data) {
            data.Setup();
            cardTitle.text = data.cardName;
            cardDescription.text = data.GetFrontDescriptions();
            cardImage.sprite = data.cardIcon;
        }

        if (data.CanSelect()) {
            
        }
    }
}
