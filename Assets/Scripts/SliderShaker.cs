using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SliderShaker : MonoBehaviour {

    public float speed = 1;
    public float amount = 1;

    public LOGIC_COMPARATION comparation;
    public int shakePoint = 50;

    public Slider slider;
    public bool shakeOnRealTimeHealth = false;

    Vector2 startingPos;
    RectTransform rectTransform;

    float randomXSeed;
    float randomYSeed;

    public enum LOGIC_COMPARATION {
        LESS_THAN, MORE_THAN
    }

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        startingPos.x = rectTransform.anchoredPosition.x;
        startingPos.y = rectTransform.anchoredPosition.y;

        randomXSeed = Random.Range(0f, 1f);
        randomYSeed = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update() {

        if (GameManager.Instance.lowerHealthInRealTime && shakeOnRealTimeHealth) {
            Shake(true);
        } else {
            switch (comparation) {
                case LOGIC_COMPARATION.LESS_THAN:
                    if (slider.value < shakePoint) {
                        Shake(false);
                    }
                    else {
                        DefaultValues();
                    }
                    break;
                case LOGIC_COMPARATION.MORE_THAN:
                    if (slider.value > shakePoint) {
                        Shake(false);
                    }
                    else {
                        DefaultValues();

                    }
                    break;
                default:
                    break;
            }
        }
        
    }

    private void DefaultValues() {
        rectTransform.anchoredPosition = startingPos;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    private void Shake(bool alwaysShake) {
        float valueMultiplier = alwaysShake ? 50 : (shakePoint - slider.value);
        rectTransform.anchoredPosition = new Vector2(startingPos.x + Random.Range(-1f,1f) * amount * valueMultiplier / 100, startingPos.y + Random.Range(-1f, 1f)  * amount * valueMultiplier / 100);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    
}
