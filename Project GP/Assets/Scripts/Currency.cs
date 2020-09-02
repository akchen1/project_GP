using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{

    private int CR;
    TextMeshProUGUI tmpui;
    // Start is called before the first frame update
    void Start()
    {
        
        tmpui = GetComponent<TextMeshProUGUI>();
        DeleteData(); // TEMP
        CR = PlayerPrefs.GetInt("CR");
    }

    // Update is called once per frame
    void Update()
    {
        tmpui.SetText("CR: " + CR.ToString());

    }

    public bool updateGold(int amount)
    {
        if (CR + amount < 0)
        {
            return false;
        }
        else
        {
            CR += amount;
            PlayerPrefs.SetInt("CR", CR);
            PlayerPrefs.Save();
            return true;
        }
    }

    public void DeleteData()
    {
        CR = 0;
        updateGold(CR);
    }

}
