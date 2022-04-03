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
    }
    public List<CardDataWeight> deckCards;
    public List<CardDataWeight> shitCards;


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
        ShuffleDeck();
    }

    public static CardData GetCardData(List<CardDataWeight> dataWeights) {
        int totalWeight = 0;
        foreach (CardDataWeight dataWeight in dataWeights)
            totalWeight += dataWeight.weight;

        int randomNumber = Random.Range(0, totalWeight);

        CardDataWeight selectedDataWeight = new CardDataWeight();
        foreach (CardDataWeight dataWeight in dataWeights) {
            if (randomNumber < dataWeight.weight) {
                selectedDataWeight = dataWeight;
                break;
            }

            randomNumber = randomNumber - dataWeight.weight;
        }

        CardData data = selectedDataWeight.data;
        if (data.isDepleted) {
            return GetCardData(dataWeights);
        }

        return data;
    }

    public void ShuffleDeck() {
        foreach (Card card in cards) {
            Destroy(card.gameObject);
        }
        cards.RemoveAll(c => true);

        //LayoutRebuilder.ForceRebuildLayoutImmediate(cardPanel);

        List<CardData> chosenCards = new List<CardData>();
        bool hasTax = false;
        for (int i = 0; i < cardsToGenerate; i++) {
            CardData data = GetCardData(deckCards);
            while (data.type == GameManager.CardType.TAX) {
                if (hasTax)
                    data = GetCardData(deckCards);
                else {
                    hasTax = true;
                    break;
                }
            }
            chosenCards.Add(data);
        }
        foreach (CardData cardData in chosenCards) {
            InstantiateCard(cardData);
        }
        bool cantPickAny = true;
        foreach (Card card in cards) {
            if (card.data.CanSelect(card)) {
                cantPickAny = false;
                break;
            }
        }
        if (cantPickAny) {
            int rand = Random.Range(0, cardsToGenerate);
            CardData shitData = GetCardData(shitCards);
            Destroy(cards[rand].gameObject);
            cards[rand] = InstantiateCard(shitData);
        }

        foreach (Card card in cards) {
            ;
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
