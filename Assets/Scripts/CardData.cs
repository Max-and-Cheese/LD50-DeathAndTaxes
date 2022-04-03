using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject {
    public string cardName;
    public Sprite cardIcon;
    public GameManager.CardType type;

    public CardAction[] selectActions;
    public CardAction[] avoidActions;

    public void RunActions (bool wasSelected) {
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

    public void Setup() {
        for (int i=0; i<selectActions.Length; i++) {
            selectActions[i] = Instantiate(selectActions[i]);
            selectActions[i].SetUpAction(this);
        }

        for (int i = 0; i < avoidActions.Length; i++) {
            avoidActions[i] = Instantiate(avoidActions[i]);
            avoidActions[i].SetUpAction(this);
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
            desc += "• ";
            desc += actionDesc;
        }
        return desc;
    }

}
