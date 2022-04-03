using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/Health Action")]
public class HealthCardAction : CardAction
{
    public int minHealthValue;
    public int maxHealthValue;

    private int healthValue;

    public override void SetUpAction(CardData data) {
        healthValue = Random.Range(minHealthValue, maxHealthValue + 1);
    }

    public override bool CanDoAction(CardData data) {
        return GameManager.Instance.Health + healthValue >= 0;
    }

    public override void DoAction(CardData data) {
        GameManager.Instance.Health += healthValue;
    }

    public override string GetDescription(CardData data) {
        return (healthValue > 0 ? "Increases":"Lowers") + " health by " + Mathf.Abs(healthValue).ToString();
    }

}
