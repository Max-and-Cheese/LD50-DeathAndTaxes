using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/Purchase Action")]
public class CardActionPurchase : CardAction
{
    public int minBasePrice;
    public int maxBasePrice;

    private int basePrice;

    public int GetPrice(CardData data) {
        return (int)(basePrice * GameManager.Instance.GetDiscountForType(data.type));
    }

    public override void SetUpAction(CardData data) {
        basePrice = Random.Range(minBasePrice, maxBasePrice+1);
    }

    public override bool CanDoAction(CardData data) {
        return GameManager.Instance.Money - GetPrice(data) >= 0;
    }

    public override void DoAction(CardData data) {
        GameManager.Instance.SpendMoney(GetPrice(data));
    }

    public override string GetDescription(CardData data) {
        return "- " + GetPrice(data).ToString() + "$";
    }

}
