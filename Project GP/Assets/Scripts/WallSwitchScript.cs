using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitchScript : MonoBehaviour
{
    // True = On, False = Off
    public bool state;
    public GameObject wall;

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
            PlayerController pScript = collision.gameObject.GetComponent<PlayerController>();
            pScript.touchWallSwitch = true;
            this.tag = "IsTouching";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController pScript = collision.gameObject.GetComponent<PlayerController>();
            pScript.touchWallSwitch = false;
            this.tag = "WallSwitch";
        }
    }

    public void OpenDoor()
    {
        wall.SetActive(false);
    }

    public void CloseDoor()
    {
        wall.SetActive(true);
    }
}
