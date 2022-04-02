using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public TMP_Text moneyRenderText;
    public Slider healthSlider;
    public Slider policeSlider;
    public GameObject gameOverGroup;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnHealthUpdated.AddListener(OnHealthUpdated);
        GameManager.Instance.OnPoliceUpdated.AddListener(OnPoliceUpdated);
        GameManager.Instance.OnMoneyUpdated.AddListener(OnMoneyUpdated);
        GameManager.Instance.OnGameOverEvent.AddListener(OnGameOver);
        OnHealthUpdated(GameManager.Instance.Health);
        OnPoliceUpdated(GameManager.Instance.Police);
        OnMoneyUpdated(GameManager.Instance.Money);
    }

    private void OnHealthUpdated(int value) {
        if (healthSlider)
        healthSlider.value = value;
    }

    private void OnPoliceUpdated(int value) {
        if (policeSlider)
        policeSlider.value = value;
    }

    private void OnMoneyUpdated(int value) {
        if (moneyRenderText)
        moneyRenderText.text = value.ToString() + "$";   
    }

    private void OnGameOver() {
        if (gameOverGroup)
            gameOverGroup.SetActive(true);
    }

}
