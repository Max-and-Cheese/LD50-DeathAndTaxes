using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private int money = 50;
    private int health = 100;
    private int police = 0;

    public int Money { get { return money; } private set { money = value; if (value != money) OnMoneyUpdated?.Invoke(value);  } }

    public int Health { get => health; set { if (value ==  0 ) { EndGameDeath();  } else if (value > 0 && value <= 100) { health = value; OnHealthUpdated?.Invoke(value); } } }

    public int Police { get => police; set { if (value == 100) { EndGameCaught(); } else if (value >= 0 && value < 100) { police = value; OnPoliceUpdated?.Invoke(value); } } }

    public int Karma { get; set; }

    public bool GAME_OVER = false;

    private int dayCount = 1;

    public HealthUpdateEvent OnHealthUpdated;
    public PoliceUpdateEvent OnPoliceUpdated;
    public MoneyUpdateEvent OnMoneyUpdated;
    public DiscountUpdateEvent OnDiscountUpdated;
    public UnityEvent OnGameOverEvent;

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
        STATUS_EFFECT,
        POLICE,
        CURSED
    };

    private Dictionary<CardType, float> activeDiscounts = new Dictionary<CardType, float>();

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

    public void AddTemporalDiscount(CardType type, float discount, int turnLength) {
        AddDiscount(type, discount);
    }

    public void ClearDiscount(CardType type) {
        OnDiscountUpdated?.Invoke(type);
        activeDiscounts.Remove(type);
    }

    private void EndGameDeath() {
        GameOver();
    }

    private void EndGameCaught() {
        GameOver();
    }

    private void GameOver() {
        GAME_OVER = true;
        OnGameOverEvent?.Invoke();
    }

    private void Awake() {
        Instance = this;
        if (OnMoneyUpdated == null)
            OnMoneyUpdated = new MoneyUpdateEvent();
        if (OnDiscountUpdated == null)
            OnDiscountUpdated = new DiscountUpdateEvent();
        if (OnPoliceUpdated == null)
            OnPoliceUpdated = new PoliceUpdateEvent();
        if (OnHealthUpdated == null)
            OnHealthUpdated = new HealthUpdateEvent();
        if (OnGameOverEvent == null)
            OnGameOverEvent = new UnityEvent();
    }

    void Start() {
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