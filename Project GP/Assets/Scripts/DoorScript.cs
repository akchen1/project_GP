using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerScript.touchDoor = true;
            this.tag = "IsTouching";
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerScript.touchDoor = false;
            this.tag = "TeleportDoor";
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
