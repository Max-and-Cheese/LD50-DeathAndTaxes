using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/AllMoneyPurchaseAction")]
public class AllMoneyPurchaseCardAction : CardAction {

    public override bool CanDoAction(Card card) { return true;}

    public override void DoAction(Card card) {
        GameManager instance = GameManager.Instance;
        instance.SpendMoney(instance.Money);
    }

    public override string GetDescription(Card card) {
        return "Cost: " + GameManager.Instance.Money.ToString() + "$";
    }

}
