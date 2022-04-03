using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [Serializable]
    public struct CardDataWeight {
        public CardData data;
        public int weight;
    }
    public List<CardDataWeight> deckCards;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
