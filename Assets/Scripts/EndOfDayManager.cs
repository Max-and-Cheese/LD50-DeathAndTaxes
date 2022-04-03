using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDayManager : MonoBehaviour
{
    public static EndOfDayManager Instance { get; private set; }

    public TMP_Text titleText;
    public TMP_Text incomeText;
    public TMP_Text summaryText;
    public Button continueButton;

    private void OnEnable() {
        titleText.text = "End of day "+GameManager.Instance.DayCount.ToString();
        incomeText.text = "End of day "+GameManager.Instance.DayCount.ToString();
    }

    private void Awake() {
        Instance = this;
    }
    
    void Start()
    {
    }

    public void OnContinue() {
        GameManager.Instance.RestartDay();
        gameObject.SetActive(false);
    }

}
