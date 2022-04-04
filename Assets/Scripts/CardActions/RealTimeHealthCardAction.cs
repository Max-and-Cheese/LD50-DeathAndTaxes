using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/RealTimeHealthAction")]
public class RealTimeHealthCardAction : CardAction {

    public int decreaseSpeed = 1;

    public override bool CanDoAction(Card data) {
        return true;
    }

    public override void DoAction(Card data) {
        GameManager.Instance.lowerHealthInRealTime = true;
        GameManager.Instance.decreaseSpeed = decreaseSpeed;
    }

    public override string GetDescription(Card data) {
        return "Can you survive?";
    }
}
