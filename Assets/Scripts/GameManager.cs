using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int money;
    private int health;
    private int police;

    public int Money {
        get { return money; }
        private set {
            if (value != money)
                OnMoneyUpdated?.Invoke(value);
            money = value;
        }
    }

    public int Health { get => health; set { health = value; OnHealthUpdated.Invoke(value); } }

    public int Police { get => police; set { police = value; OnPoliceUpdated.Invoke(value); } }

    public HealthUpdateEvent OnHealthUpdated;
    public PoliceUpdateEvent OnPoliceUpdated;
    public MoneyUpdateEvent OnMoneyUpdated;
    public DiscountUpdateEvent OnDiscountUpdated;

    //If a negative value is inputed, the function acts like SpendMoney(int)
    public bool AddMoney(int count) {
        if (count < 0)
            return SpendMoney(-count);
        else if (count == 0) return true;

        Money += count;

        return true;
    }
    //Returns false if the operation fails (Spending more money than available)
    //If a negative value is inputed, the function acts like AddMoney(int)
    public bool SpendMoney(int count) {
        if (count < 0)
            return AddMoney(-count);
        else if (count == 0) return true;

        if (Money - count >= 0) {
            Money -= count;
            return true;
        }

        return false;
    }


    public enum CardType {
        HEALTH,
        TAX,
        CARD_EFFECT,
        STATUS_EFFECT
    };

    private Dictionary<CardType, float> activeDiscounts;

    public float GetDiscountForType (CardType type) {
        bool isDiscount = activeDiscounts.TryGetValue(type, out float discount);
        return isDiscount ? discount : 1;
    }

    public void AddDiscount (CardType type, float discount) {
        OnDiscountUpdated?.Invoke(type);
        bool isDiscount = activeDiscounts.TryGetValue(type, out float existingDiscount);
        if (isDiscount) {
            activeDiscounts.Add(type, existingDiscount * discount);
        } else {
            activeDiscounts.Add(type, discount);
        }
    }

    public void ClearDiscount(CardType type) {
        OnDiscountUpdated?.Invoke(type);
        activeDiscounts.Remove(type);
    }

    void OnEnable() {
        Instance = this;
        if (OnMoneyUpdated == null)
            OnMoneyUpdated = new MoneyUpdateEvent();
        if (OnDiscountUpdated == null)
            OnDiscountUpdated = new DiscountUpdateEvent();
        if (OnPoliceUpdated == null)
            OnPoliceUpdated = new PoliceUpdateEvent();
        if (OnHealthUpdated == null) 
            OnHealthUpdated = new HealthUpdateEvent();
        
    }

    void Start() {
        Health = 100;
        Police = 0;
        Money = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}

[System.Serializable]
public class MoneyUpdateEvent : UnityEvent<int> { }

[System.Serializable]
public class HealthUpdateEvent : UnityEvent<int> { }

[System.Serializable]
public class PoliceUpdateEvent : UnityEvent<int> { }



[System.Serializable]
public class DiscountUpdateEvent : UnityEvent<GameManager.CardType> { }