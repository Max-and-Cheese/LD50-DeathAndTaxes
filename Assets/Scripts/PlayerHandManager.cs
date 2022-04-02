using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandManager : MonoBehaviour {

    public HorizontalLayoutGroup group;
    public RectTransform mainRect;

    private int targetPadding = -100;
    private int targetSpacing = -150;
    private float duration = 3;
    //private float timer = 0;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        //group.padding.bottom = Mathf.CeilToInt(Mathf.Lerp(group.padding.bottom, targetPadding, timer / duration));
        //timer = timer + Time.deltaTime;
    }

    public void ShowHand() {
        StopAllCoroutines();
        targetPadding = 10;
        targetSpacing = 30;
        StartCoroutine(HandRoutine());

    }

    public void HideHand() {
        StopAllCoroutines();
        targetPadding = -100;
        targetSpacing = -150;
        StartCoroutine(HandRoutine());
    }

    private IEnumerator HandRoutine() {
        print("Routine Started");
        float timer = 0;
        while (group.padding.bottom != targetPadding) {
        print(timer);
            group.padding.bottom = Mathf.CeilToInt(Mathf.Lerp(group.padding.bottom, targetPadding, timer / duration));
            group.spacing = Mathf.CeilToInt(Mathf.Lerp(group.spacing, targetSpacing, timer / duration));
            timer = timer + Time.deltaTime;
            LayoutRebuilder.ForceRebuildLayoutImmediate(mainRect);

            yield return null;
        }
    }
}
