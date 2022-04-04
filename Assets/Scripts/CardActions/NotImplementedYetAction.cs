using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/NotImplementedYetAction")]
public class NotImplementedYetAction : CardAction {
    public override bool CanDoAction(Card card) { return true; }

    public override void DoAction(Card card) {

    }

    public override string GetDescription(Card card) {
        return "This card is not implemented yet, sorry!";
    }
}
