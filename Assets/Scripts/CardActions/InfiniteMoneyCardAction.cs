using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/InfiniteMoneyAction")]
public class InfiniteMoneyCardAction : CardAction {
    
    public override bool CanDoAction(Card card) { return true; }

    public override void DoAction(Card card) {
        GameManager.Instance.infiniteMoney = true;
    }

    public override string GetDescription(Card card) {
        return "No more running out of money";
    }

}
