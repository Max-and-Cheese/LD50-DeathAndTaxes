using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/Karma Action")]
public class KarmaCardAction : CardAction
{
    public int karmaValue;

    public override void SetUpAction(CardData data) {}

    public override bool CanDoAction(CardData data) { return true;}

    public override void DoAction(CardData data) {
        GameManager.Instance.Karma += karmaValue;
    }

    public override string GetDescription(CardData data) {
        return (karmaValue > 0 ? "+":"-") + Mathf.Abs(karmaValue).ToString() + " karma";
    }

}
