using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/BurnCardAction")]
public class BurnCardAction : CardAction {

    public override bool CanDoAction(Card data) {
        return true;
    }

    public override void DoAction(Card data) {
        GameManager manager = GameManager.Instance;
        manager.TurnClicks += 1;
        manager.destroyNextCard = true;
    }

    public override string GetDescription(Card data) {
        return "Select a card from the deck to burn and reroll";
    }
}
