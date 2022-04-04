using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "Cards/Actions/CallMomAction")]
public class CallMomAction : CardAction {

    public int randMoney;
    public int policeThreshold;
    public CardData guaranteedCardOnThreshold;
    public int health;
    public int police;

    public override bool CanDoAction(Card data) {
        return true;
    }

    public override void DoAction(Card data) {
        GameManager manager = GameManager.Instance;
        if (manager.Police > policeThreshold) {
            OpportunityController.Instance.GuaranteeCard(guaranteedCardOnThreshold);
            return;
        }
        int rand = data.RandSeed % 4;
        switch (rand) {
            default:
            case 0:
                manager.RevenueOfDay += randMoney;
                return;
            case 1:
                manager.Health += health;
                return;
            case 2:
                manager.Police += police;
                return;
            case 3:
                OpportunityController.Instance.GuaranteeCard(guaranteedCardOnThreshold);
                return;
        }
    }

    public override string GetDescription(Card data) {
        return "Call your mom and see what she can help you with";
    }
    
}
