using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/GetOutOfJailAction")]
public class GetOutOfJailAction : CardAction {

    public int policeValue;

    public override bool CanDoAction(Card data) { return true; }

    public override void DoAction(Card data) {
       GameManager.Instance.Police = policeValue;
    }

    public override string GetDescription(Card data) {
        return "Keep this card in your hand to avoid getting caught!";
    }
}
