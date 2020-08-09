using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIScript : MonoBehaviour
{
    TextMeshProUGUI tmpui;
    
    // Start is called before the first frame update
    void Start()
    {
        tmpui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tmpui.SetText("Health: " + playerScript.health);
    }
}
