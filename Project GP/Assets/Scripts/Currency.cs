using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{

    private int gold;
    TextMeshProUGUI tmpui;
    // Start is called before the first frame update
    void Start()
    {
        tmpui = GetComponent<TextMeshProUGUI>();
        gold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tmpui.SetText("Gold: " + gold.ToString());

    }

    public bool updateGold(int amount)
    {
        if (gold + amount < 0)
        {
            return false;
        }
        else
        {
            gold += amount;
            return true;
        }
    }

}
