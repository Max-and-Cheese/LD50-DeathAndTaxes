using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject {
    public string cardName;
    public string cardDescription;
    public Sprite cardIcon;
    public GameManager.CardType type;

    public CardAction[] selectActions;
    public CardAction[] avoidActions;

    public void RunAction (bool wasSelected) {
        if (wasSelected) {
            if (selectActions != null && selectActions.Length > 0) {
                if (CanSelect()) {
                    foreach (CardAction action in selectActions) {
                        action.DoAction(this);
                    }
                }
            }
        }
        else {
            if (avoidActions != null && avoidActions.Length > 0) {
                foreach (CardAction action in avoidActions) {
                    if (action.CanDoAction(this))
                        action.DoAction(this);
                }
            }
        }
    }

    public bool CanSelect() {
        foreach (CardAction action in selectActions) {
            if (!action.CanDoAction(this)) {
                return false;
            }
        }
        return true;
    }

    public string GetFrontDescriptions() {
        return GetDesc(selectActions);
    }

    public string GetBackDescriptions() {
        return GetDesc(avoidActions);
    }

    private string GetDesc(CardAction[] actions) {
        string desc = "";
        bool first = true;
        foreach (CardAction action in actions) {
            string actionDesc = action.GetDescription(this);
            if (first) first = false;
            else if (actionDesc.Length > 0) {
                desc += "\n";
            }
            desc += actionDesc;
        }
        return desc;
    }

}
