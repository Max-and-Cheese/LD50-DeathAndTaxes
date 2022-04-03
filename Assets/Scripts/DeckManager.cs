using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckManager : MonoBehaviour {
    public static DeckManager Instance { get; private set; }

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

        return selectedDataWeight.data;
    }

    public void ShuffleDeck() {
        foreach (Card card in cards) {
            Destroy(card);
        }
        cards.RemoveAll(c => true);
        List<CardData> chosenCards = new List<CardData>();
        for (int i = 0; i < cardsToGenerate; i++) {
            chosenCards.Add(GetCardData(deckCards));
        }
        bool cantPickAny = true;
        foreach (CardData cardData in chosenCards) {
            if (cardData.CanSelect()) {
                cantPickAny = false;
                break;
            }
        }
        if (cantPickAny) {
            int rand = Random.Range(0, cardsToGenerate);
            chosenCards[rand] = GetCardData(shitCards);
        }

        foreach (CardData cardData in chosenCards) {
            Card card = Instantiate(cardPrefab);
            card.data = cardData;
            cards.Add(card);
            card.SetUpData();
        }
    }
}
