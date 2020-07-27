using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    bool isGrounded;

    // SerializeField makes it so I can see the Transform groundCheck variable in the Unity Inspector, so I can assign it a value outside of my code.
    // Unity actually throws a warning since we never assign it a value in our code and cannot tell that we assigned it a value in the inspector. This warning can be completely ignored.
    [SerializeField]
    Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the player is on the ground. This works by "shooting" an imaginary line downwards, from the players position to the position of the invisible groundCheck object.
        // If this raycast line hits an object that is ground, it changes the boolean isGrounded to true.
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Checks if the "d" key is being pressed
        if (Input.GetKey("d"))
        {
            // Changes the x-axis velocity of the player while retaining the y-axis velocity
            rbody.velocity = new Vector2(3, rbody.velocity.y);
        }

        // Checks if the "a" key is being pressed
        else if (Input.GetKey("a"))
        {
            // Changes the x-axis velocity of the player while retaining the y-axis velocity
            rbody.velocity = new Vector2(-3, rbody.velocity.y);
        }

        // This else statement is to set the player's x-axis velocity to 0 if neither "a" nor "d" are being pressed.
        // Without this statement, the player would glide.
        else
        {
            // Set player x-axis velocity to 0 while retaining y-axis velocity
            rbody.velocity = new Vector2(0, rbody.velocity.y);
        }

        // Check if the space key is pressed AND that the player is on the ground
        if (Input.GetKey("space") && isGrounded)
        {
            // Retain current x-axis velocity, while adding a bit of y-axis velocity
            rbody.velocity = new Vector2(rbody.velocity.x, 5);
        }
    }
}
