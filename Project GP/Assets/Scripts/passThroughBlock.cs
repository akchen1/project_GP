using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passThroughBlock : MonoBehaviour
{
    private PlatformEffector2D effector;
    private GameObject player;
    private PlayerController playerScript;
    private Collider2D blockColl;
    private Collider2D playerColl;
    private bool flip = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerColl = player.GetComponent<Collider2D>();
        playerScript = player.GetComponent<PlayerController>();
        effector = GetComponent<PlatformEffector2D>();
        blockColl = this.gameObject.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("RobotPet").GetComponent<BoxCollider2D>(), true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flip = true;
        }


        if (flip && !blockColl.IsTouching(playerColl))
        {
            flip = false;
            blockColl.isTrigger = false;
        }


        if (playerScript.getCurrentPassThroughBlock() != this.gameObject || playerScript.getCurrentPassThroughBlock() == null)
        {
            blockColl.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        blockColl.isTrigger = false;
    }
}
