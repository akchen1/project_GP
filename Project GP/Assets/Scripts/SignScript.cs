using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When touching door
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerScript.touchSign = true;
            this.tag = "IsTouching";
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // When not touching door
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerScript.touchSign = false;
            this.tag = "Untagged";
            transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().SetText("Press F To Read Sign");
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
