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
    public ParticleSystem burnParticles;

    public bool isActive = false;

    public int RandSeed { get; private set; }

    public void SetUpData() {
        if (data) {
            cardTitle.text = data.cardName;
            cardDescription.text = data.GetFrontDescriptions(this);
            cardBackDescription.text = data.GetBackDescriptions(this);
            cardImage.sprite = data.cardIcon;
            if (!data.CanSelect(this) && OpportunityController.Instance.UIcard != this) {
                cardTitle.color = Color.red;
            }
        }
    }
    
    void Start() {
        GenerateSeed();
        SetUpData();
    }

    public void GenerateSeed() {
        RandSeed = Random.Range(0, int.MaxValue);
    }

    public void RunCardActions(bool wasSelected) {
        if (!wasSelected) Flip();
        data.RunActions(wasSelected, this);
    }

    public bool wasSelected = false;

    public void Flip() {
        targetRotation = 180;
        cardButton.interactable = false;
        StartCoroutine(FlipRoutine());
        ParticlesBurnDelayed(0.75f);
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

    private float timer;
    private float timerTarget = 0;
    private float particleLenght = 0.6f;
    private void FixedUpdate() {
        if (timerTarget > 0) {
            timer += Time.deltaTime;
            if (timer >= timerTarget) {
                burnParticles.Play();
                if (timer >= timerTarget + particleLenght) {
                    burnParticles.Stop();
                    timerTarget = 0;
                    timer = 0;
                    Back.SetActive(false);
                }
            }
        }
    }

    public void ParticlesBurnDelayed(float seconds) {
        timerTarget = seconds + Random.Range(-0.1f, 0.1f);
    }

    public void OnClicked() {
        GameManager manager = GameManager.Instance;
        if (manager.GAME_OVER || manager.IsWaiting()) return;
        
        if (manager.destroyNextCard) {
            if (isActive)
                return;
            else {
                DeckManager.Instance.ReDrawCard(this);
                manager.destroyNextCard = false;
            }
        } else if (!wasSelected && data && data.CanSelect(this)) {
            wasSelected = true;
            RunCardActions(true);
            manager.TurnClicks -= 1;
            if (isActive) {
                PlayerHandManager.Instance.RemoveCard(transform.GetSiblingIndex());
            }
        }
    }
}
