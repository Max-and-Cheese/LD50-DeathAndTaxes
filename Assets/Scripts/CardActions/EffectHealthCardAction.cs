using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/HealthEffectAction")]
public class EffectHealthCardAction : CardAction {

    public int healthModifier;
    public int turnLength;

    public override bool CanDoAction(Card data) {return true;}

    public override void DoAction(Card data) {
        GameManager manager = GameManager.Instance;
        manager.DailyHealthLoss -= healthModifier;
        manager.AddTimedAction(()=> manager.DailyHealthLoss += healthModifier, turnLength);
    }

    public override string GetDescription(Card data) {
        return "Improves health for "+turnLength.ToString()+" days";
    }

}
