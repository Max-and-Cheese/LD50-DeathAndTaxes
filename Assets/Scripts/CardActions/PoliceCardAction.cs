using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/PoliceAction")]
public class PoliceCardAction : CardAction
{
    public int minPoliceValue;
    public int maxPoliceValue;

    public int GetValue(Card card) {
        return minPoliceValue + (card.randSeed % (maxPoliceValue + 1));
    }

    public override bool CanDoAction(Card data) { return true; }

    public override void DoAction(Card data) {
        GameManager.Instance.Police += GetValue(data);
    }

    public override string GetDescription(Card data) {
        int value = GetValue(data);
        return (value > 0 ? "Increases":"Lowers") + " police attention by " + Mathf.Abs(value).ToString();
    }

}
