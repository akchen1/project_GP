using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HighscoreTimer : MonoBehaviour
{

    float gameTimer;
    string timerStr;

    string minutes;
    string seconds;

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

            if (timeSpan.Minutes < 10)
            {
                minutes = "0" + timeSpan.Minutes.ToString();
            }

            else
            {
                minutes = timeSpan.Minutes.ToString();
            }

            if (timeSpan.Seconds < 10)
            {
                seconds = "0" + timeSpan.Seconds.ToString();
            }

            else
            {
                seconds = timeSpan.Seconds.ToString();
            }

            timerStr = "Time: " + minutes + ":" + seconds;
            
            tmpui.SetText(timerStr);
        }
       
    }
}
