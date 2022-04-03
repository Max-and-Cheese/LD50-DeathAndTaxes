using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/KarmaAction")]
public class KarmaCardAction : CardAction
{
    public int karmaValue;
    
    public override bool CanDoAction(Card data) { return true;}

    public override void DoAction(Card data) {
        GameManager.Instance.Karma += karmaValue;
    }

    public override string GetDescription(Card data) {
        return (karmaValue > 0 ? "+":"-") + Mathf.Abs(karmaValue).ToString() + " karma";
    }

}
