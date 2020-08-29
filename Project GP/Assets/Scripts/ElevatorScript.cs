using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    GameObject leftWall;
    GameObject rightWall;

    bool onAFloor;

    public int currentFloor;

    // Start is called before the first frame update
    void Start()
    {
        leftWall = transform.GetChild(0).gameObject;
        rightWall = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenDoor(string door)
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

    void CloseDoor(string door)
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

    void GoToFloor(int floor)
    {
        if (floor - currentFloor > 0)
        {

        }

        else if (floor - currentFloor < 0)
        {

        }

        else
        {
            // Same floor
            OpenDoor("Left");
        }
    }
}
