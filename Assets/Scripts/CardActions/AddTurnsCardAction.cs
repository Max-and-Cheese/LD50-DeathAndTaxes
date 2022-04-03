using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/AddTurnsAction")]
public class AddTurnsCardAction : CardAction
{
    public int turnsAdded;
    
    public override bool CanDoAction(Card data) { return true; }

    public override void DoAction(Card data) {
        GameManager.Instance.TurnClicks += turnsAdded;
    }

    public override string GetDescription(Card data) {
        return "Allows the selection of " + turnsAdded.ToString() + " cards instead of 1";
    }

}
