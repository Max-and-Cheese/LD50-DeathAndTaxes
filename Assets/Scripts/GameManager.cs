using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    private int money = 10;
    private int health = 100;
    private int police = 0;

    public int Money { get { return money; } private set { money = value; OnMoneyUpdated?.Invoke(value); } }

    public int Health { get => health; set { if (value == 0) { EndGameDeath(); } health = value; OnHealthUpdated?.Invoke(Mathf.Clamp(value, 0, 100)); } }

    public int Police { get => police; set { if (value == 100) { EndGameCaught(); } police = value; OnPoliceUpdated?.Invoke(Mathf.Clamp(value, 0, 100)); } }

    public int Karma { get; set; }

    public bool GAME_OVER = false;

    private int turnClicks = 1;
    public int TurnClicks { get => turnClicks; set { turnClicks = value; if (value == 0) { TurnEnded(); } } }

    public int DayCount { get; private set; } = 1;

    public int RevenueOfDay { get; set; }
    public int DailyRevenue { get; set; } = 10;
    public int DailyHealthLoss { get; set; } = 3;
    public int DailyPoliceLoss { get; set; } = 5;

    public HealthUpdateEvent OnHealthUpdated;
    public PoliceUpdateEvent OnPoliceUpdated;
    public MoneyUpdateEvent OnMoneyUpdated;
    public DiscountUpdateEvent OnDiscountUpdated;
    public UnityEvent OnDayEndEvent;
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
        POLICE,
        MONEY,
        SHIT_CARD,
        CARD_EFFECT,
        STATUS_EFFECT,
    };

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
        if (OnDayEndEvent == null)
            OnDayEndEvent = new UnityEvent();
    }

    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }


    //DISCOUNTS

    private Dictionary<CardType, float> activeDiscounts = new Dictionary<CardType, float>();

    public float GetDiscountForType(CardType type) {
        bool isDiscount = activeDiscounts.TryGetValue(type, out float discount);
        return isDiscount ? discount : 1;
    }

    public void AddDiscount(CardType type, float discount) {
        OnDiscountUpdated?.Invoke(type);
        bool isDiscount = activeDiscounts.TryGetValue(type, out float existingDiscount);
        if (isDiscount) {
            activeDiscounts.Add(type, existingDiscount * discount);
        } else {
            activeDiscounts.Add(type, discount);
        }
    }

    public void AddTemporalDiscount(CardType type, float discount, int turnLength) {
        OnDiscountUpdated?.Invoke(type);
        AddDiscount(type, discount);
        AddTimedAction(()=>AddDiscount(type, 1f/discount), turnLength);
    }

    public void ClearDiscount(CardType type) {
        OnDiscountUpdated?.Invoke(type);
        activeDiscounts.Remove(type);
    }


    //GAME OVER AND RESET

    private void EndGameDeath() {
        PlayerPrefs.SetString("cause", "Death finally caught on to you!");
        GameOver();
    }

    private void EndGameCaught() {
        PlayerPrefs.SetString("cause", "The police threw your ass in jail!");
        GameOver();
    }

    private void GameOver() {
        GAME_OVER = true;
        OnGameOverEvent?.Invoke();
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("days", DayCount);
        SceneManager.LoadScene("GameOver");
    }


    private void TurnEnded() {
        DeckManager.Instance.RunAvoidedCards();
        RevenueOfDay += DailyRevenue;
        
        DelayActionInmediate(()=>EndOfDayManager.Instance.ShowPanel(), 2);
    }

    public void RestartDay() {
        CashInRevenueOfDay();
        DayCount++;
        Health = Mathf.Clamp(Health - DailyHealthLoss, 0, 100);
        Police = Mathf.Clamp(Police - DailyPoliceLoss, 0, 100);
        turnClicks = 1;
        DeckManager.Instance.ShuffleDeck();
        OpportunityController.Instance.AttemptOpportunity();
    }

    private void CashInRevenueOfDay() {
        Money += RevenueOfDay;
        RevenueOfDay = 0;
    }


    // TIMED EVENTS

    private List<TimedAction> timedActions = new List<TimedAction>();

    public void AddTimedAction(Action action, int waitDays) {
        timedActions.Add(new TimedAction(action, DayCount, waitDays));
    }

    private void CheckForActions() {
        List<TimedAction> runActions = new List<TimedAction>();
        foreach (TimedAction timedAction in timedActions) {
            if (timedAction.CanExecute(DayCount)) {
                timedAction.ExecuteAction();
                runActions.Add(timedAction);
            }
        }
        timedActions.RemoveAll(ta=>runActions.Contains(ta));

    }

    // DELAYS

    private float timer = 0;
    private Action waitingAction;
    private float waitSeconds = 0;
    private void FixedUpdate() {
        if (waitingAction != null) {
            timer += Time.deltaTime;
            if (timer >= waitSeconds) {
                waitingAction();
                waitingAction = null;
                waitSeconds = 0;
                timer = 0;
            }
        }
    }

    public void DelayActionInmediate(Action action, float seconds) {
        if (waitingAction == null) {
            waitingAction = action;
            waitSeconds = seconds;
        }
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

public delegate void Action();

public class TimedAction {

    private int dayStarted;
    private int waitLength;
    private Action action;
    public TimedAction (Action action, int start, int length) {
        this.action = action;
        dayStarted = start;
        waitLength = length;
    }

    public bool CanExecute(int currentDay) {
        return currentDay - dayStarted > waitLength;
    }

    public void ExecuteAction() {
        action();
    }

}