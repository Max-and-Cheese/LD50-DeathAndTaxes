using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/AddTurnsAction")]
public class AddTurnsCardAction : CardAction
{
    public int turnsAdded;
    
    public override bool CanDoAction(Card card) {
        int possibleCards = 0;
        foreach (Card cardSel in DeckManager.Instance.cards) {
            if (cardSel.data.CanSelect(card)) possibleCards++;
        }
        return possibleCards >= turnsAdded;
    }

    public override void DoAction(Card card) {
        GameManager.Instance.TurnClicks += turnsAdded;
    }

    public override string GetDescription(Card card) {
        return "Allows the selection of " + turnsAdded.ToString() + " cards instead of 1";
    }

}
