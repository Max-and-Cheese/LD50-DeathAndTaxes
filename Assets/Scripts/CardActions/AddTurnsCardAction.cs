using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/Add Turns Action")]
public class AddTurnsCardAction : CardAction
{
    public int turnsAdded;

    public override void SetUpAction(CardData data) {}

    public override bool CanDoAction(CardData data) { return true; }

    public override void DoAction(CardData data) {
        GameManager.Instance.TurnClicks += turnsAdded;
    }

    public override string GetDescription(CardData data) {
        return "Allows the selection of " + turnsAdded.ToString() + " cards instead of 1";
    }

}
