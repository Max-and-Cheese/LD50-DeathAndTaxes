using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/HealthAction")]
public class HealthCardAction : CardAction
{
    public int minHealthValue;
    public int maxHealthValue;

    public int GetValue(Card card) {
        return minHealthValue + (card.randSeed % (maxHealthValue + 1));
    }

    public override bool CanDoAction(Card data) { return true; }

    public override void DoAction(Card data) {
        GameManager.Instance.Health += GetValue(data);
    }

    public override string GetDescription(Card data) {
        int value = GetValue(data);
        return (value > 0 ? "Increases":"Lowers") + " health by " + Mathf.Abs(value).ToString();
    }

}
