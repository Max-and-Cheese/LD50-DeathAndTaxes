using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public RectTransform mainRect;
    public Button cardButton;
    public GameObject Back;

    public TMP_Text cardTitle;
    public TMP_Text cardDescription;
    public TMP_Text cardBackDescription;
    public Image cardImage;

    public CardData data;

    public void SetUpData() {
        if (data) {
            data = Instantiate(data);
            data.Setup();
            cardTitle.text = data.cardName;
            cardDescription.text = data.GetFrontDescriptions();
            cardBackDescription.text = data.GetBackDescriptions();
            cardImage.sprite = data.cardIcon;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void RunCardActions(bool wasSelected) {
        if (!wasSelected) Flip();
        data.RunActions(wasSelected);
    }

    public bool wasSelected = false;

    public void Flip() {
        targetRotation = 180;
        cardButton.interactable = false;
        StartCoroutine(FlipRoutine());
    }

    private float targetRotation = 0;
    private readonly float duration = 3;

    private float currentRotation = 0;
    private bool rotationIsOverMid = false;
    private IEnumerator FlipRoutine() {
        float timer = 0;
        while (currentRotation < targetRotation) {


            float lerpRot = Mathf.LerpAngle(currentRotation, targetRotation, timer / duration);
            if (currentRotation <= 90 && lerpRot >= 90) {
                rotationIsOverMid = true;
                Back.SetActive(true);
                cardButton.gameObject.SetActive(false);
            }
            currentRotation = lerpRot;
            if (rotationIsOverMid) {
                var rot = Back.transform.rotation;
                Back.transform.rotation = Quaternion.Euler(0, 180 - currentRotation, 0);
            } else {
                var rot = cardButton.transform.rotation;
                cardButton.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
            }

            timer = timer + Time.deltaTime;
            LayoutRebuilder.ForceRebuildLayoutImmediate(mainRect);

            yield return null;
        }
        
    }

    public void OnClicked() {
        if (GameManager.Instance.GAME_OVER) return;
        
        if (data && data.CanSelect()) {
            wasSelected = true;
            RunCardActions(true);
            GameManager.Instance.TurnClicks -= 1;
        }
    }
}
