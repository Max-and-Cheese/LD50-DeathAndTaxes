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
    public GameObject blackBackground;
    
    private void Awake() {
        Instance = this;
        blackBackground.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnContinue() {
        GameManager.Instance.RestartDay();
        HidePanel();
    }

    public RectTransform dayPanel;
    private float targetPosition = 0;
    private float duration = 30;
    private bool setToHide = false;

    private IEnumerator AnimatePanelRoutine() {
        float timer = 0;
        while (Mathf.Floor(dayPanel.anchoredPosition.y) != targetPosition) {
            dayPanel.anchoredPosition = new Vector2(0, Mathf.Lerp(dayPanel.anchoredPosition.y, targetPosition, timer / duration));
            timer += Time.deltaTime;
            LayoutRebuilder.ForceRebuildLayoutImmediate(dayPanel);
            yield return null;
        }
        if (setToHide) {
            gameObject.SetActive(false);
        }
    }

    public void ShowPanel() {
        gameObject.SetActive(true);
        titleText.text = "End of day " + GameManager.Instance.DayCount.ToString();
        incomeText.text = "You earned " + GameManager.Instance.RevenueOfDay.ToString() + "$";

        targetPosition = 0;
        setToHide = false;
        StartCoroutine(AnimatePanelRoutine());
    }
    public void HidePanel() {
        targetPosition = -400;
        setToHide = true;
        StartCoroutine(AnimatePanelRoutine());
    }

}
