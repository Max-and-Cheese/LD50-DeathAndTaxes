using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/Police Action")]
public class PoliceCardAction : CardAction
{
    public int minPoliceValue;
    public int maxPoliceValue;

    private int policeValue;

    public override void SetUpAction(CardData data) {
        policeValue = Random.Range(minPoliceValue, maxPoliceValue + 1);
    }

    public override bool CanDoAction(CardData data) {
        return GameManager.Instance.Police + policeValue >= 0;
    }

    public override void DoAction(CardData data) {
        GameManager.Instance.Police += policeValue;
    }

    public override string GetDescription(CardData data) {
        return (policeValue > 0 ? "Increases":"Lowers") + " police attention by " + Mathf.Abs(policeValue).ToString();
    }

}
