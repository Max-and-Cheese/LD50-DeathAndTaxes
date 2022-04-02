using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    private string playerName;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPlayerName(string Name){
        playerName = Name;
    }

    public void EscenaJuego(){
        if(string.IsNullOrEmpty(playerName))
            Debug.Log("Ingrese su nombre");
        else
            SceneManager.LoadScene("Juego");
    }


}
