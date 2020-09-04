using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HighscoreTimer : MonoBehaviour
{

    float gameTimer;
    string timerStr;

    TextMeshProUGUI tmpui;

    // used to help with formatting the time into min, seconds and miliseconds
    TimeSpan timeSpan;

    // Start is called before the first frame update
    void Start()
    {
        tmpui = GetComponent<TextMeshProUGUI>();
        gameTimer = 0f;


    }

    // Update is called once per frame
    void Update()
    {
        // pauses when player pauses the game
        if (!PauseMenu.isPaused){

            gameTimer += Time.deltaTime;

            timeSpan = TimeSpan.FromSeconds(gameTimer); 
            timerStr = String.Format("Time: {0:00}:{1:00}.{2:0}", timeSpan.Minutes, timeSpan.Seconds, (timeSpan.Milliseconds).ToString()[0]);

            tmpui.SetText(timerStr);
        }
       
    }
}
