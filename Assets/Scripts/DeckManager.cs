using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; set; }

    [Serializable]
    public struct CardDataWeight {
        public CardData data;
        public int weight;
    }
    public List<CardDataWeight> deckCards;

    public int cardsToGenerate = 3;

    public List<Card> cards;

    public void RunAvoidedCards () {
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
