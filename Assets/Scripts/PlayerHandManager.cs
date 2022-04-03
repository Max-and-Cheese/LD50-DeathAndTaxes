using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandManager : MonoBehaviour {

    public static PlayerHandManager Instance;

    public HorizontalLayoutGroup group;
    public RectTransform mainRect;
    public GameObject cardPrefab;

    List<CardData> playerHand;

    private int targetPadding = -100;
    private int targetSpacing = -150;
    private float duration = 3;
    // Start is called before the first frame update
    void Start() {
        playerHand = new List<CardData>();
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
        playerHand.Add(card);
        UpdateHandUI();
    }

    private void UpdateHandUI() {
        foreach (Transform card in group.gameObject.transform) {
            Destroy(card.gameObject);
        }

        foreach (CardData card in playerHand) {
            GameObject newCard = Instantiate(cardPrefab);
            var cardComponent = newCard.GetComponent<Card>();
            cardComponent.data = card;
            cardComponent.SetUpData();
            newCard.transform.SetParent(group.gameObject.transform, false);
        }
    }
}
