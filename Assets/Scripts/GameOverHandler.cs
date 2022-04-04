using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{


    public TMP_Text causeText;
    public TMP_Text dayMoneyText;

    private int days;
    private int money;
    private string cause;
    // Start is called before the first frame update
    void Start()
    {
        days = PlayerPrefs.GetInt("days");
        money = PlayerPrefs.GetInt("money");
        cause = PlayerPrefs.GetString("cause");

        dayMoneyText.text = $"You lasted {days} days having evaded {money}$ of taxes!";
        causeText.text = cause;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
