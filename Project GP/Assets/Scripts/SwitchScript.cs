using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public Sprite switchOn;
    public Sprite switchOff;

    public SpriteRenderer spriteRenderer;

    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        // Access SpriteRender attatched and set switch to off sprite initially
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = switchOff;
        isOn = false;
    }

    private void Update()
    {
        if (isOn)
        {
            spriteRenderer.sprite = switchOn;
        }

        else
        {
            spriteRenderer.sprite = switchOff;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerScript.touchSwitch = true;
            this.tag = "IsTouching";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerScript.touchSwitch = false;
            this.tag = "Switch";
        }
    }
}
