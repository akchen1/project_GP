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
    float rollDelay;


    // Check if on ladder
    bool onLadder;

    // Get Animation for taking damage
    Animation anim;
    Animator animator;
    
    // temp animation for rolling
    public Sprite rollSprite; 
    public Sprite mainSprite; 

    // Check if touching interactable
    public bool touchSign;
    public bool touchDoor;
    public bool touchSwitch;
    public bool touchWallSwitch;
    public bool touchPuzzleSwitch;

    private GameObject currentPassThroughBlock;
    private float doubleTapDownTimer = 0.5f;
    private int doubleTapDownCount = 0;

    // animation stuff
    private bool isFacingRight;

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
        animator = gameObject.GetComponent<Animator>();
        invincibleTimer = 0f;
        rollDelay = 0f;

        touchSign = false;
        touchSwitch = false;
        touchWallSwitch = false;
        touchPuzzleSwitch = false;
        onLadder = false;

        isFacingRight = true;
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

        // Delay on rolling so you can't keep rolling
        if (rollDelay > 0)
        {
            rollDelay -= Time.deltaTime;
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

            // Check if door is open

            else if (touchDoor)
            {
                // Determine which door is which
                GameObject originDoor = GameObject.FindGameObjectWithTag("IsTouching");
                DoorScript doorScript = originDoor.GetComponent<DoorScript>();

                if (doorScript.isOpen)
                {
                    GameObject destDoor = GameObject.FindGameObjectWithTag("TeleportDoor");
                    transform.position = destDoor.transform.position;
                }
                
            }

            // Check if touching a switch
            else if (touchSwitch)
            {
                SwitchScript switchScript = GameObject.FindGameObjectWithTag("IsTouching").GetComponent<SwitchScript>();

                // Change status of switch
                if (switchScript.isOn)
                {
                    switchScript.isOn = false;
                }
                else
                {
                    switchScript.isOn = true;
                }
            }

            // Check if touching wall switch
            else if (touchWallSwitch)
            {
                WallSwitchScript wsScript = GameObject.FindGameObjectWithTag("IsTouching").GetComponent<WallSwitchScript>();
                if (wsScript.state)
                {
                    wsScript.state = false;
                    wsScript.OpenDoor();
                }
                else
                {
                    wsScript.state = true;
                    wsScript.CloseDoor();
                }
            }

            else if (touchPuzzleSwitch)
            {
                PuzzleSwitchScript psScript = GameObject.FindGameObjectWithTag("IsTouching").GetComponent<PuzzleSwitchScript>();
                if (psScript.state)
                {
                    psScript.state = false;
                }
                else
                {
                    psScript.state = true;
                }
            }
        }

        // Checks if the "d" key is being pressed
        if (Input.GetKey("d") && !isRoll)
        {
            // Changes the x-axis velocity of the player while retaining the y-axis velocity
            rbody.velocity = new Vector2(3, rbody.velocity.y);

            // Look Right
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            
        }

        // Checks if the "a" key is being pressed
        else if (Input.GetKey("a") && !isRoll)
        {
            // Changes the x-axis velocity of the player while retaining the y-axis velocity
            rbody.velocity = new Vector2(-3, rbody.velocity.y);

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
            if (!onMovingPlatform && !isRoll)
            {
                // Set player x-axis velocity to 0 while retaining y-axis velocity
                rbody.velocity = new Vector2(0, rbody.velocity.y);
            }

            else if (onMovingPlatform)
            {
                // Set player velocity to moving platform velocity
                rbody.velocity = new Vector2(mPVel, rbody.velocity.y);
            }
        }

        // Check if the space key is pressed AND that the player is on the ground
        if (Input.GetKey("space") && onGround)
        {
            // Retain current x-axis velocity, while adding a bit of y-axis velocity
            rbody.velocity = new Vector2(rbody.velocity.x, 7f);
        }

        isPassThroughBlock();
        // Double tap down key to go down a pass through block
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (doubleTapDownTimer > 0 && doubleTapDownCount == 1 && currentPassThroughBlock != null/*Number of Taps you want Minus One*/)
            {
                currentPassThroughBlock.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else
            {
                doubleTapDownTimer = 0.5f;
                doubleTapDownCount += 1;
            }
        }
        if (doubleTapDownTimer > 0)
        {
            doubleTapDownTimer -= 1 * Time.deltaTime;
        }
        else
        {
            doubleTapDownCount = 0;
        }

        // Check if the "shift" key is pressed
        // Can only roll if grounded
        if (Input.GetKey(KeyCode.LeftShift) && isRoll == false && rollDelay <= 0)
        {
            // play the roll animation

            // Player is invisible for the duration of the roll
            isRoll = true; 
            rollTimer = 0.3f; 
            invincibleTimer = 0.3f;
            takingDamage = true;
            rollDelay = 2f;
                
            //direction of roll
            // rolls right
            if (transform.localScale.x > 0){
                rbody.velocity = new Vector2(10, rbody.velocity.y);
            }
            // rolls left
            else if (transform.localScale.x < 0){
                rbody.velocity = new Vector2(-10, rbody.velocity.y);
            }
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

        animationStates();
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

    private void animationStates()
    {
        if (weapon != null)
        {
            animator.SetBool("hasGun", true);
        }
        else
        {
            animator.SetBool("hasGun", false);
        }
        if (rbody.velocity.x < 0)
        {
            animator.SetBool("isRunning", true);
            if (isFacingRight)
            {
                flip();
            }

        } else if (rbody.velocity.x > 0)
        {
            animator.SetBool("isRunning", true);
            if (!isFacingRight)
            {
                flip();
            }
        } else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void flip()
    {
        
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;


    }

    public GameObject getCurrentPassThroughBlock()
    {
        return currentPassThroughBlock;
    }

    // Check if player is going to land on a pass through block
    private bool isPassThroughBlock()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position - new Vector3(coll.bounds.size.x / 2, coll.bounds.size.y / 2, 0), Vector2.down, 1, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position + new Vector3(coll.bounds.size.x / 2, -coll.bounds.size.y / 2, 0), Vector2.down, 1, groundLayer);
        // If it collides with something that isn't NULL
        if (leftHit.collider != null)
        {
            if (leftHit.collider.tag == "passThroughBlock")
            {
                currentPassThroughBlock = leftHit.collider.gameObject;
            }
            else
            {
                currentPassThroughBlock = null;
            }
            return true;
        }
        // If left side isn't, check right side
        else if (rightHit.collider != null)
        {
            if (rightHit.collider.tag == "passThroughBlock")
            {
                currentPassThroughBlock = rightHit.collider.gameObject;
            }
            else
            {
                currentPassThroughBlock = null;
            }
            return true;
        }
        else
        {
            currentPassThroughBlock = null;
        }
        return false;
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
        } else if (collision.gameObject.tag == "passThroughBlock")
        {
            currentPassThroughBlock = collision.gameObject;
        }
        else
        {
            currentPassThroughBlock = null;
        }

        if (transform.position.y - (coll.bounds.size.y / 2) >= collision.gameObject.transform.position.y + (collision.gameObject.GetComponent<BoxCollider2D>().bounds.size.y / 2))
        {
            fallTimer = 5f;
            onGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Finish") || collision.gameObject.CompareTag("MovingPlatform") || collision.gameObject.tag == "passThroughBlock")
        {
            if (transform.position.y - (coll.bounds.size.y / 2) >= collision.gameObject.transform.position.y + (collision.gameObject.GetComponent<BoxCollider2D>().bounds.size.y / 2))
            {
                fallTimer = 5f;
                onGround = true;
            }

            if (collision.gameObject.CompareTag("MovingPlatform"))
            {
                // If they player hits a moving platform add velocity to move player along with platform
                // Update moving platform velocity constantly
                mPVel = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            }

            else if (collision.gameObject.tag == "passThroughBlock")
            {
                currentPassThroughBlock = collision.gameObject;
            }
            else
            {
                currentPassThroughBlock = null;
            }
        }        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Finish") || collision.gameObject.CompareTag("MovingPlatform") || collision.gameObject.tag == "passThroughBlock")
        {
            onGround = false;
            onMovingPlatform = false;
            currentPassThroughBlock = null;
        } 
        
    }
}
