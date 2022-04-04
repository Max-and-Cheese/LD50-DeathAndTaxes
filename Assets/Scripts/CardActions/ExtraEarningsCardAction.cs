using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/ExtraEarningsAction")]
public class ExtraEarningsCardAction : CardAction {

    public int extraEarnings;

    public override bool CanDoAction(Card data) {
        return true;
    }

    public override void DoAction(Card data) {
        GameManager.Instance.DailyRevenue += extraEarnings;
    }

    public override string GetDescription(Card data) {
        return "Extra "+extraEarnings.ToString()+"$ per day";
    }
}
