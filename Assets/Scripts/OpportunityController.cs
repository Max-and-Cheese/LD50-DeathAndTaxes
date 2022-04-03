using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpportunityController : MonoBehaviour {

    public static OpportunityController Instance { get; private set; }

    public RectTransform cardPanel;
    public Card UIcard;
    public CardData cardData;

    public GameObject blackBackground;
    private float targetPosition = 0;
    private float duration = 3;
    private bool setToHide = false;
    
    void Awake(){
        Instance = this;
        if (cardData) {
            UpdateCard(cardData);
        }
    }

    // Update is called once per frame
    void Update() {

        blackBackground.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnEnable() {
        //ShowPanel();

    }

    private IEnumerator AnimatePanelRoutine() {
        float timer = 0;
        while (Mathf.Ceil(cardPanel.anchoredPosition.x) != targetPosition) {
            cardPanel.anchoredPosition = new Vector2(Mathf.Lerp(cardPanel.anchoredPosition.x, targetPosition, timer / duration), 0);
            timer += Time.deltaTime;
            LayoutRebuilder.ForceRebuildLayoutImmediate(cardPanel);
            yield return null;
        }
        if (setToHide) {
            gameObject.SetActive(false);
        }
    }

    public void ShowPanel() {
        gameObject.SetActive(true);
        targetPosition = 0;
        setToHide = false;
        StartCoroutine(AnimatePanelRoutine());
    }
    public void HidePanel() {
        targetPosition = 300;
        setToHide = true;
        StartCoroutine(AnimatePanelRoutine());
    }

    public void AttemptOpportunity() {

    }

    public void AcceptOportunity() {
        PlayerHandManager.Instance.AddCardToHand(cardData);
        HidePanel();
    }

    public void UpdateCard(CardData cardData) {
        this.cardData = cardData;
        UIcard.data = cardData;
        UIcard.SetUpData();
    }

}
