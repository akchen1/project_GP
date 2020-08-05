using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject bullet;

    // Variables to keep track of how fast the player can shoot
    float shootTimer;
    
    // This variable is how fast the player can shoot (ie. if shootSpeed is set to 1, player can shoot once every 1 second)
    float shootSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Find Rigidbody and Collider
        rbody = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        // Assign starting values to variables
        fallTimer = 5f;
        shootTimer = 0f;
        shootSpeed = 0.5f;
        pos = transform.position;
    }

    // Function that checks if the player is on the ground
    bool IsGrounded()
    {
        // Create a raycast directly below the player
        // I am only making the raycast go from the position of the player, down to the height of the player divided by 1.9f
        // This is so it checks if the ground is JUST underneath the player
        // I also use 1.9f because if I divide the height of the player by 2, the raycast will only go to the very edge of the player so that wouldn't work
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, coll.bounds.size.y / 1.9f, groundLayer);

        // If it collides with something that isn't NULL
        if (hit.collider != null)
        {
            // This checks if it collides with a block that is actually the finish line
            if (hit.collider.gameObject.tag == "Finish")
            {
                // If the player hits the finish line, reload the scene to generate a new level
                // This line of code will be super useful later on, to move from the start menu, to switch levels, etc.
                SceneManager.LoadScene("SampleScene");
            }

            // Reset timer
            fallTimer = 5f;
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the "d" key is being pressed
        if (Input.GetKey("d"))
        {
            // Changes the x-axis velocity of the player while retaining the y-axis velocity
            rbody.velocity = new Vector2(5, rbody.velocity.y);
        }

        // Checks if the "a" key is being pressed
        else if (Input.GetKey("a"))
        {
            // Changes the x-axis velocity of the player while retaining the y-axis velocity
            rbody.velocity = new Vector2(-5, rbody.velocity.y);
        }

        // This else statement is to set the player's x-axis velocity to 0 if neither "a" nor "d" are being pressed.
        // Without this statement, the player would glide.
        else
        {
            // Set player x-axis velocity to 0 while retaining y-axis velocity
            rbody.velocity = new Vector2(0, rbody.velocity.y);
        }

        // Check if the space key is pressed AND that the player is on the ground
        if (Input.GetKey("space") && IsGrounded())
        {
            // Retain current x-axis velocity, while adding a bit of y-axis velocity
            rbody.velocity = new Vector2(rbody.velocity.x, 5);
        }

        // Check if shoot timer is above 0, meaning the player can't shoot
        if (shootTimer > 0f)
        {
            // Keep counting down
            shootTimer -= Time.deltaTime;
        }

        // Set shoot timer to 0 for consistency, meaning player can shoot
        else
        {
            shootTimer = 0f;
        }

        // Check if enter key is pressed
        if (Input.GetKey("return") && shootTimer == 0f)
        {
            // Fire bullet
            Instantiate(bullet, transform.position, Quaternion.identity);
            shootTimer = shootSpeed;
        }

        // If player is not on the ground, countdown from timer
        if (!IsGrounded())
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
            // Reset player back to starting position
            transform.position = pos;

            // Reset velocity and rotation
            rbody.velocity = new Vector2(0, 0);
            rbody.rotation = 0;
            rbody.angularVelocity = 0;

            // Reset timer
            fallTimer = 5f;
        }
    }
}
