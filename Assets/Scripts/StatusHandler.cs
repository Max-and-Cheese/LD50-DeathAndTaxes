using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text moneyText;
    public Slider healthSlider;
    public Slider policeSlider;

    private GameManager manager;
    void Start()
    {
        manager = GameManager.Instance;
        SetMoney(manager.Money);
        SetHealth(manager.Health);
        SetPolice(manager.Police);
        manager.OnPoliceUpdated.AddListener(SetPolice); 
        manager.OnHealthUpdated.AddListener(SetHealth);
        manager.OnMoneyUpdated.AddListener(SetMoney);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.lowerHealthInRealTime) {
            SetMoney(Random.Range(1000, 9999));
        }
    }

    private void SetMoney(int money) {
        moneyText.text = $"{money}$";
    }

    private void SetHealth(int health) {
        healthSlider.value = health;
    }
    private void SetPolice(int police) {
        policeSlider.value = police;
    }
}
