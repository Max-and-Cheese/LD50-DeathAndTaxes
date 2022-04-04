using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/ExtraWorkAction")]
public class WorkCardAction : CardAction {

    public int minExtraWork;
    public int maxExtraWork;

    public int GetValue(Card card) {
        if (minExtraWork == maxExtraWork) return minExtraWork;
        return minExtraWork + (card.RandSeed % (maxExtraWork - minExtraWork + 1));
    }

    public override bool CanDoAction(Card card) { return true; }

    public override void DoAction(Card card) {
        GameManager.Instance.RevenueOfDay += GetValue(card);
    }

    public override string GetDescription(Card card) {
        int value = GetValue(card);
        if (value < 0) return GetValue(card).ToString() + "$ less at the end of the day";
        return "Extra " + GetValue(card).ToString() + "$ at the end of the day";
    }

}
