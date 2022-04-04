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
        GameManager manager = GameManager.Instance;
        manager.lowerHealthInRealTime = true;
        manager.decreaseSpeed = decreaseSpeed;
        manager.diabloVolume.SetActive(true);
    }

    public override string GetDescription(Card data) {
        return "Can you survive?";
    }
}
