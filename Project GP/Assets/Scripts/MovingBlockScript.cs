using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockScript : MonoBehaviour
{
    // Bunch of variables
    Rigidbody2D rbody;
    public float moveSpeed;
    public float moveDistance;

    // Start and end positions
    Vector3 startPos;
    Vector3 endPos;

    // Bool that will tell when platform is moving left or right
    bool moveRight;

    // Start is called before the first frame update
    void Start()
    {
        // Get stuff
        rbody = GetComponent<Rigidbody2D>();

        // Calculate stuff
        startPos = transform.position;
        endPos = startPos + new Vector3(moveDistance, 0, 0);

        // Initialize variable
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure the platform doesn't rotate and doesn't move up or down
        rbody.angularVelocity = 0f;
        transform.position = new Vector2(transform.position.x, startPos.y);

        // If the platform is at the end of it's destination, it's on the right
        if (transform.position.x >= endPos.x)
        {
            // Make it start moving left
            moveRight = false;
        }

        // If the platform is at the beginning of it's path, it's on the left
        if (transform.position.x <= startPos.x)
        {
            // Make it start moving right
            moveRight = true;
        }

        if(moveRight)
        {
            // Move Right
            rbody.velocity = new Vector2(moveSpeed, 0);
        }

        else if (!moveRight)
        {
            // Move Left
            rbody.velocity = new Vector2(-moveSpeed, 0);
        }
        
    }
}
