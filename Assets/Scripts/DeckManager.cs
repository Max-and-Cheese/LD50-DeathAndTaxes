using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DeckManager : MonoBehaviour {
    public static DeckManager Instance { get; private set; }

    public RectTransform cardPanel;
    [Serializable]
    public struct CardDataWeight {
        public CardData data;
        public int weight;
        public int minDay;
    }
    public List<CardDataWeight> deckCards;
    public List<CardDataWeight> shitCards;

    private List<CardData> depletedCards;
    public void AddDepletedCard (CardData data) {
        depletedCards.Add(data);
    }

    public int cardsToGenerate = 3;

    public Card cardPrefab;
    public List<Card> cards;

    public void RunAvoidedCards() {
        foreach (Card card in cards) {
            if (!card.wasSelected) {
                card.RunCardActions(false);
            }
        }
    }

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        depletedCards = new List<CardData>();
        ShuffleDeck();
    }

    public static CardData GetCardData(List<CardDataWeight> dataWeights) {
        int totalWeight = 0;
        int day = GameManager.Instance.DayCount;
        foreach (CardDataWeight dataWeight in dataWeights) {
            if (dataWeight.minDay <= day)
                totalWeight += dataWeight.weight;
        }
        
        int randomNumber = Random.Range(0, totalWeight);

        CardDataWeight selectedDataWeight = new CardDataWeight();
        foreach (CardDataWeight dataWeight in dataWeights) {
            if (dataWeight.minDay <= day) {
                if (randomNumber < dataWeight.weight) {
                    selectedDataWeight = dataWeight;
                    break;
                }
                randomNumber = randomNumber - dataWeight.weight;
            }
        }

        CardData data = selectedDataWeight.data;
        //Debug.Log(skipCards.Contains(data));
        //if (data.isDepleted || skipCards.Contains(data)) {
        //    return GetCardData(dataWeights, skipCards);
        //}

        return data;
    }

    public void ShuffleDeck() {
        foreach (Card card in cards) {
            Destroy(card.gameObject);
        }
        cards.RemoveAll(c => true);

        //LayoutRebuilder.ForceRebuildLayoutImmediate(cardPanel);

        List<CardData> chosenCards = new List<CardData>();
        for (int i = 0; i < cardsToGenerate; i++) {
            CardData data = null;
            while (data == null || depletedCards.Contains(data) || chosenCards.Contains(data)){
                data = GetCardData(deckCards);
            }
            chosenCards.Add(data);
        }
        foreach (CardData cardData in chosenCards) {
            InstantiateCard(cardData);
        }
        bool cantPickAny = true;
        foreach (Card card in cards) {
            Debug.Log(card.data.cardName + " : " + card.data.CanSelect(card));
            if (card.data.CanSelect(card)) {
                cantPickAny = false;
                break;
            }
        }
        if (cantPickAny) {
            int rand = Random.Range(0, cardsToGenerate);
            CardData shitData = null;
            while (shitData == null || depletedCards.Contains(shitData)) {
                shitData = GetCardData(shitCards);
            }
            Destroy(cards[rand].gameObject);
            cards[rand] = InstantiateCard(shitData);
        }
        

    }

    private Card InstantiateCard(CardData data) {
        Card card = Instantiate(cardPrefab, transform.position, transform.rotation, transform);
        card.data = data;
        cards.Add(card);
        card.GenerateSeed();
        card.SetUpData();
        return card;
    }
}
