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

    public bool isUnique = false;

    public bool isDepleted { get; private set; } = false;

    public void RunActions (bool wasSelected, Card card) {
        if (wasSelected) {
            if (selectActions != null && selectActions.Length > 0) {
                if (CanSelect(card)) {
                    foreach (CardAction action in selectActions) {
                        action.DoAction(card);
                    }
                }
            }
            if (isUnique) isDepleted = true;
        }
        else {
            if (avoidActions != null && avoidActions.Length > 0) {
                foreach (CardAction action in avoidActions) {
                    if (action.CanDoAction(card))
                        action.DoAction(card);
                }
            }
        }
    }

    public bool CanSelect(Card card) {
        foreach (CardAction action in selectActions) {
            if (!action.CanDoAction(card)) {
                return false;
            }
        }
        return true;
    }

    public string GetFrontDescriptions(Card card) {
        return GetDesc(selectActions, card);
    }

    public string GetBackDescriptions(Card card) {
        return GetDesc(avoidActions, card);
    }

    private string GetDesc(CardAction[] actions, Card card) {
        string desc = "";
        bool first = true;
        foreach (CardAction action in actions) {
            string actionDesc = action.GetDescription(card);
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
