using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardAction : ScriptableObject {

    public abstract void SetUpAction(CardData data);

    public abstract bool CanDoAction(CardData data);

    public abstract void DoAction(CardData data);

    public abstract string GetDescription(CardData data);
}
