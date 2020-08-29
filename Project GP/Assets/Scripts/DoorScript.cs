using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Sprite doorClosed;
    public Sprite doorOpened;

    [SerializeField]
    GameObject[] switches;

    public bool isOpen;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // If there are no switches associated with door, set door to be open
        if (switches.Length == 0)
        {
            isOpen = true;
            spriteRenderer.sprite = doorOpened;
        }

        // If all switches associated with door are open, set door to be open
        else if (allSwitchesOpen()) 
        {
            isOpen = true;
            spriteRenderer.sprite = doorOpened;
        }

        else
        {
            isOpen = false;
            spriteRenderer.sprite = doorClosed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (allSwitchesOpen())
        {
            isOpen = true;
            spriteRenderer.sprite = doorOpened;
        }

        else
        {
            isOpen = false;
            spriteRenderer.sprite = doorClosed;
        }
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

    public bool allSwitchesOpen()
    {
    // Iterates through all associated switches. If all swithches are on, return true
    // else return false

        for (int i=0; i < switches.Length; i++)
        {
            if (switches[i].GetComponent<SwitchScript>().isOn == false)
            {
                return false;
            }
        }

        return true;
    }

}
