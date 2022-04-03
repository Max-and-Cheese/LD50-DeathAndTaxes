using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardAction : ScriptableObject {

    public abstract bool CanDoAction(Card data);

    public abstract void DoAction(Card data);

    public abstract string GetDescription(Card data);
}
