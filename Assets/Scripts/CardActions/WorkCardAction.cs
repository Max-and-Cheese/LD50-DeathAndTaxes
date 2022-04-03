using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/ExtraWorkAction")]
public class WorkCardAction : CardAction {

    public int minExtraWork;
    public int maxExtraWork;

    public int GetValue(Card card) {
        return minExtraWork + (card.RandSeed % (maxExtraWork + 1));
    }

    public override bool CanDoAction(Card card) { return true; }

    public override void DoAction(Card card) {
        GameManager.Instance.RevenueOfDay += GetValue(card);
    }

    public override string GetDescription(Card card) {
        return "Extra " + GetValue(card).ToString() + "$ at the end of the day";
    }

}
