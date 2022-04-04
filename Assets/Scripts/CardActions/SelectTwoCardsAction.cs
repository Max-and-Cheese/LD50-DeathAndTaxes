using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/SelectTwoCardsAction")]
public class SelectTwoCardsAction : CardAction {

    public int randMoney;
    public int policeThreshold;
    public CardData guaranteedCardOnThreshold;
    public int health;
    public int police;

    public override bool CanDoAction(Card data) {
        return true;
    }

    public override void DoAction(Card data) {
       
    }

    public override string GetDescription(Card data) {
        return "Allows ";
    }
    
}
