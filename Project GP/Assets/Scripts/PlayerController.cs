using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Get access to the player's Rigidbody and Collider
    Rigidbody2D rbody;
    Collider2D coll;

    // LayerMask for the ground
    public LayerMask groundLayer;

    // Timer variable
    float fallTimer;

    // Get starting position of player for resetting
    Vector3 pos;

    // Get bullet gameobject
    // public GameObject bullet;
    public Weapon weapon;

    // Player stats and current states
    public int health;
    Vector2 shootDirection;
    float invincibleTimer;
    bool takingDamage;
    bool isRoll;
    float rollTimer;
    bool onGround;
    bool onMovingPlatform;
    float mPVel;


    // Check if on ladder
    bool onLadder;

    // Get Animation for taking damage
    Animation anim;
    
    // temp animation for rolling
    public Sprite rollSprite; 
    public Sprite mainSprite; 

    // Check if touching interactable
    public bool touchSign;
    public bool touchDoor;

    // Start is called before the first frame update
    void Start()
    {
        // Find Rigidbody and Collider
        rbody = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        // Assign starting values to variables
        fallTimer = 5f;
        pos = transform.position;
        health = 5;
        onGround = false;
        onMovingPlatform = false;
        mPVel = 0f;

        anim = GetComponent<Animation>();
        invincibleTimer = 0f;

        touchSign = false;
        onLadder = false;
    }

    // Function for the player to take damage
    public void Hit(int damage)
    {
        // This gives the player an invincibility window where they can't be hit again
        // Currently the animation is 2.5 seconds long
        // You can check it by looking at the animation in the inspector window
        if (takingDamage == false)
        {
            // Take damage and play hit animation
            health -= damage;
            anim.Play();
            takingDamage = true;
            invincibleTimer = anim.clip.length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if animation is currently playing so player is invincible
        if (takingDamage == true)
        {
            invincibleTimer -= Time.deltaTime;
        }

        // If animation is not playing anymore
        if (invincibleTimer <= 0)
        {
            takingDamage = false;
        }

        // count down on roll timer and invisibility timer
        if (rollTimer >= 0)
        {
            rollTimer -= Time.deltaTime;

            // change sprite to something else
            gameObject.GetComponent<SpriteRenderer>().sprite = rollSprite;
        }

        else
        {
            isRoll = false;

            //change sprite back to original
            gameObject.GetComponent<SpriteRenderer>().sprite = mainSprite;
        }

        // Check if interaction key is being pressed
        if (Input.GetKeyDown("f"))
        {
            // Check if touching a sign
            if (touchSign)
            {
                // Do something here
                GameObject sign = GameObject.FindGameObjectWithTag("IsTouching");
                sign.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().SetText("This Is A Sign.");
            }

            // If touching a door
            else if (touchDoor)
            {
                // Determine which door is which
                GameObject originDoor = GameObject.FindGameObjectWithTag("IsTouching");
                GameObject destDoor = GameObject.FindGameObjectWithTag("TeleportDoor");

                // Teleport to the other door
                transform.position = destDoor.transform.position;
            }
        }

        // Checks if the "d" key is being pressed
        if (Input.GetKey("d"))
        {
            // Changes the x-axis velocity of the player while retaining the y-axis velocity
            rbody.velocity = new Vector2(5, rbody.velocity.y);

            // Look Right
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            
        }

        // Checks if the "a" key is being pressed
        else if (Input.GetKey("a"))
        {
            // Changes the x-axis velocity of the player while retaining the y-axis velocity
            rbody.velocity = new Vector2(-5, rbody.velocity.y);

            // Look Left
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }

        // This else statement is to set the player's x-axis velocity to 0 if neither "a" nor "d" are being pressed.
        // Without this statement, the player would glide.
        else
        {
            if (!onMovingPlatform)
            {
                // Set player x-axis velocity to 0 while retaining y-axis velocity
                rbody.velocity = new Vector2(0, rbody.velocity.y);
            }
            else
            {
                // Set player velocity to moving platform velocity
                rbody.velocity = new Vector2(mPVel, rbody.velocity.y);
            }
        }

        // Check if the space key is pressed AND that the player is on the ground
        if (Input.GetKey("space") && onGround)
        {
            // Retain current x-axis velocity, while adding a bit of y-axis velocity
            rbody.velocity = new Vector2(rbody.velocity.x, 7);
        }

        // Check if the "shift" key is pressed
        // Can only roll if grounded
        if (Input.GetKey(KeyCode.LeftShift) && isRoll == false){
                // play the roll animation

                // Player is invisible for the duration of the roll
                rollTimer = 1.350f; 
                invincibleTimer = 1.350f;
                takingDamage = true;
        }
        
        // If on a ladder and press W, go up
        if (Input.GetKey("w") && onLadder)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, 3);
        }

        // If on a ladder and press S, go down
        else if (Input.GetKey("s") && onLadder)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, -3);
        }

        // Check if mouse key is pressed
        if (Input.GetMouseButton(0))
        {
            // Fire bullet
            if (weapon.Shoot())
            {
                // Knockback
                shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                shootDirection.Normalize();
                rbody.AddForce((-shootDirection * weapon.recoil), ForceMode2D.Force);
            }

        }
        // Check if weapon realod is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon.Reload();
        }

        // If player is not on the ground, countdown from timer
        if (!onGround)
        {
            fallTimer -= Time.deltaTime;
        }

        // This checks if the player has been falling for over 5 seconds
        // I have a timer variable set to 5, that counts down every time it is not touching the ground
        // If it touches the ground, the timer resets back to 5 seconds
        // This just moves the player back to the starting position
        // It doesn't reset the level, so the player can keep trying the level over and over again
        if (fallTimer <= 0)
        {
            ResetPlayer();
        }

        // Check if player health is 0
        if (health <= 0)
        {
            ResetPlayer();
        }
    }

    // Function that sets the player back at the beginning with everything reset
    public void ResetPlayer()
    {
        // Reset player back to starting position
        transform.position = pos;

        // Reset velocity and rotation
        rbody.velocity = new Vector2(0, 0);
        rbody.rotation = 0;
        rbody.angularVelocity = 0;

        // Reset timer
        fallTimer = 5f;

        // Reset health
        health = 5;
    }

    // Built in Unity function that checks if it collides with a trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If hits collectible, pick it up
        if (collision.tag == "Collectible")
        {
            health++;
            Destroy(collision.gameObject);
        }

        // Checks if the player is currently on a ladder
        else if (collision.tag == "Ladder")
        {
            onLadder = true;
        }
    }

    // Built in Unity function that checks if it exits from a trigger collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Leaving ladder
        if (collision.tag == "Ladder")
        {
            onLadder = false;
        }
    }

    // Built in Unity function that checks while it is in a trigger collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Still staying in ladder
        if (collision.tag == "Ladder")
        {
            // This makes it so the player can stay on the ladder without timing out due to not touching the ground
            // However they will not be able to jump off the ladder
            fallTimer = 5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            // If the player hits the finish line, reload the scene to generate a new level
            // This line of code will be super useful later on, to move from the start menu, to switch levels, etc.
            SceneManager.LoadScene("SampleScene");
        }

        else if (collision.gameObject.tag == "MovingPlatform")
        {
            // If they player hits a moving platform add velocity to move player along with platfomr
            mPVel = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            onMovingPlatform = true;
        }

        fallTimer = 5f;
        onGround = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Finish") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            fallTimer = 5f;
            onGround = true;

            if (collision.gameObject.CompareTag("MovingPlatform"))
            {
                // If they player hits a moving platform add velocity to move player along with platform
                // Update moving platform velocity constantly
                mPVel = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            }
        }        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Finish") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            onGround = false;
            onMovingPlatform = false;
        }
    }
}
