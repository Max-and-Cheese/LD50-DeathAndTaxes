using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/PurchaseAction")]
public class PurchaseCardAction : CardAction
{
    public int minBasePrice;
    public int maxBasePrice; 
    
    public int GetPrice(Card card) {
        int basePrice = minBasePrice + (card.RandSeed%(maxBasePrice+1));
        return (int)(basePrice * GameManager.Instance.GetDiscountForType(card.data.type));
    }

    public override bool CanDoAction(Card card) {
        return GameManager.Instance.Money - GetPrice(card) >= 0;
    }

    public override void DoAction(Card card) {
        GameManager.Instance.SpendMoney(GetPrice(card));
    }

    public override string GetDescription(Card card) {
        return "Cost: " + GetPrice(card).ToString() + "$";
    }

}
