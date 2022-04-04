using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/HealthAction")]
public class HealthCardAction : CardAction
{
    public int minHealthValue;
    public int maxHealthValue;

    public int GetValue(Card card) {
        if (minHealthValue == maxHealthValue) return minHealthValue;
        return minHealthValue + ((int)Mathf.Sign(maxHealthValue)) * (card.RandSeed % (Mathf.Abs(maxHealthValue - minHealthValue + 1)));
    }

    public override bool CanDoAction(Card data) { return true; }

    public override void DoAction(Card data) {
        GameManager.Instance.Health += GetValue(data);
    }

    public override string GetDescription(Card data) {
        int value = GetValue(data);
        return (value > 0 ? "+":"-") + Mathf.Abs(value).ToString() + " health";
    }

}
