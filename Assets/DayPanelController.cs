using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayPanelController : MonoBehaviour
{

    public RectTransform dayPanel;
    private float targetPosition = 0;
    private float duration = 30;
    private bool setToHide = false;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    void OnEnable() {
        ShowPanel();

    }

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
