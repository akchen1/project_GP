using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;

    bool onAFloor;
    bool moveUp;
    bool moveDown;

    public int currentFloor;
    Vector3 newFloorPos;

    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        leftWall = transform.GetChild(0).gameObject;
        rightWall = transform.GetChild(1).gameObject;

        currentFloor = 1;

        moveUp = false;
        moveDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveUp)
        {
            if (transform.position.y < newFloorPos.y)
            {
                rbody.velocity = new Vector2(0, 3);
                onAFloor = false;
            }

            else
            {
                moveUp = false;
                rbody.velocity = Vector2.zero;
                onAFloor = true;
            }
        }

        else if (moveDown)
        {
            if (transform.position.y > newFloorPos.y)
            {
                rbody.velocity = new Vector2(0, -3);
                onAFloor = false;
            }

            else
            {
                moveDown = false;
                rbody.velocity = Vector2.zero;
                onAFloor = true;
            }
        }
    }

    public void OpenDoor(string door)
    {
        if (door == "Left")
        {
            leftWall.SetActive(false);
        }

        else if (door == "Right")
        {
            rightWall.SetActive(false);
        }
    }

    public void CloseDoor(string door)
    {
        if (door == "Left")
        {
            leftWall.SetActive(true);
        }

        else if (door == "Right")
        {
            rightWall.SetActive(true);
        }
    }

    public void GoToFloor(int floor)
    {
        if (floor - currentFloor > 0)
        {
            newFloorPos = new Vector3(0, (floor - currentFloor) * 5, 0) + transform.position;
            moveUp = true;
            currentFloor = floor;
            
        }

        else if (floor - currentFloor < 0)
        {
            // Go To Lower Floor
            newFloorPos = new Vector3(0, (floor - currentFloor) * 5, 0) + transform.position;
            moveDown = true;
            currentFloor = floor;
        }

        else
        {
            // Same floor
        }
    }
}
