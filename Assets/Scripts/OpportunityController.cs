using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DeckManager;

public class OpportunityController : MonoBehaviour {

    public static OpportunityController Instance { get; private set; }

    public RectTransform cardPanel;
    public Card UIcard;
    public CardData cardData;

    [Range(0, 100)]
    public int oportunityChance = 20;
    public List<CardDataWeight> oppportunityCards;

    public GameObject blackBackground;
    private float targetPosition = 0;
    private float duration = 3;
    private bool setToHide = false;

    private CardData guaranteedCard = null;

    public void GuaranteeCard (CardData data) {
        guaranteedCard = data;
    }
    public bool HasGuaranteedCard() {
        return guaranteedCard != null;
    }

    void Awake() {
        Instance = this;
        if (cardData) {
            UpdateCard(cardData);
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        //blackBackground.SetActive(true);
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
        CardData card = null;
        if (HasGuaranteedCard()) {
            card = guaranteedCard;
            guaranteedCard = null;
        } else if (Random.Range(0, 100) < oportunityChance) {
            card = DeckManager.GetCardData(oppportunityCards);
        }
        if (card != null) {
            UpdateCard(card);
            ShowPanel();
        }
    }

    public void AcceptOportunity() {
        PlayerHandManager.Instance.AddCardToHand(cardData);
        HidePanel();
    }

    public void UpdateCard(CardData cardData) {
        this.cardData = cardData;
        UIcard.data = cardData;
        UIcard.GenerateSeed();
        UIcard.SetUpData();
    }

}
