using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/Discount Action")]
public class DiscountCardAction : CardAction
{
    public GameManager.CardType type;
    public float discount;
    public int turnLength;
   
    public override void SetUpAction(CardData data) {}

    public override bool CanDoAction(CardData data) {
        return true;
    }

    public override void DoAction(CardData data) {
        if (turnLength <= 0)
            GameManager.Instance.AddDiscount(type, discount);
        else
            GameManager.Instance.AddTemporalDiscount(type, discount, turnLength);
    }

    public override string GetDescription(CardData data) {
        string desc;
        if (discount > 1) {
            desc = type.ToString() + " cards are " + (int)((discount-1)*100) + "% more expensive";
        } else {
            desc = type.ToString() + " cards are " + (int)((1 - discount) * 100) + "% off";
        }
        if (turnLength > 0) {
            desc += " for "+turnLength+" days";
        }
        return desc;
    }

}
