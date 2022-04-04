using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/GuaranteeCardAction")]
public class GuaranteeCardAction : CardAction {

    public CardData card;

    public override bool CanDoAction(Card data) {
        return !OpportunityController.Instance.HasGuaranteedCard();
    }

    public override void DoAction(Card data) {
        OpportunityController.Instance.GuaranteeCard(card);
    }

    public override string GetDescription(Card data) {
        return "Get a free "+card.cardName+" card on your next round";
    }
    
}
