using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandManager : MonoBehaviour {

    public static PlayerHandManager Instance;

    public HorizontalLayoutGroup group;
    public RectTransform mainRect;
    public Card cardPrefab;

    public List<Card> playerHand;

    private int targetPadding = -100;
    private int targetSpacing = -150;
    private float duration = 3;
    // Start is called before the first frame update
    void Start() {
        ClearCards();
        Instance = this;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ShowHand() {
        if (GameManager.Instance.GAME_OVER) return;
        StopAllCoroutines();
        targetPadding = 10;
        targetSpacing = 30;
        StartCoroutine(HandRoutine());

    }

    public void HideHand() {
        if (GameManager.Instance.GAME_OVER) return;
        StopAllCoroutines();
        targetPadding = -100;
        targetSpacing = -150;
        StartCoroutine(HandRoutine());
    }

    private IEnumerator HandRoutine() {
        float timer = 0;
        while (group.padding.bottom != targetPadding) {
            group.padding.bottom = Mathf.CeilToInt(Mathf.Lerp(group.padding.bottom, targetPadding, timer / duration));
            group.spacing = Mathf.CeilToInt(Mathf.Lerp(group.spacing, targetSpacing, timer / duration));
            timer = timer + Time.deltaTime;
            LayoutRebuilder.ForceRebuildLayoutImmediate(mainRect);

            yield return null;
        }
    }

    public void AddCardToHand(CardData card) {
        playerHand.Add(InstantiateCard(card));
        while (playerHand.Count > 5) {
            Destroy(playerHand[0].gameObject);
            playerHand.RemoveAt(0);
        }
    }

    private Card InstantiateCard(CardData data) {
        Card card = Instantiate(cardPrefab, transform.position, transform.rotation, transform);
        card.data = data;
        card.GenerateSeed();
        card.SetUpData();
        card.isActive = true;
        return card;
    }

    private void ClearCards() {
        foreach (Card card in playerHand) {
            Destroy(card.gameObject);
        }
        playerHand.RemoveAll(_ => true);
    }

    public void RemoveCard(Card targetCard) {
        for (int i = 0; i < playerHand.Count; i++) {
            if (playerHand[i].data == targetCard.data) {
                RemoveCard(i);
                return;
            }
        }
    }

    internal void RemoveCard(int cardIndex) {
        Destroy(playerHand[cardIndex].gameObject);
        playerHand.RemoveAt(cardIndex);
    }
}
