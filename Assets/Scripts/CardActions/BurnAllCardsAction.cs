using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/BurnAllCardsAction")]
public class BurnAllCardsAction : CardAction {

    public override bool CanDoAction(Card data) {
        return true;
    }

    public override void DoAction(Card data) {
        GameManager.Instance.TurnClicks += 1;
        DeckManager.Instance.ReDrawAll();
    }

    public override string GetDescription(Card data) {
        return "Reroll all cards in deck";
    }
}
