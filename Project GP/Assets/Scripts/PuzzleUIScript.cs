using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleUIScript : MonoBehaviour
{

    public bool box1State;
    public bool box2State;
    public bool box3State;

    public GameObject puzzleSwitch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (box1State && box2State && box3State)
        {
            PuzzleSwitchScript psScript = puzzleSwitch.GetComponent<PuzzleSwitchScript>();
            psScript.state = false;
            psScript.OpenDoor();
        }
    }

    public void PressBox1()
    {
        if (box1State)
        {
            box1State = false;
        }
        else
        {
            box1State = true;
        }
    }

    public void PressBox2()
    {
        if (box2State)
        {
            box2State = false;
        }
        else
        {
            box2State = true;
        }
    }

    public void PressBox3()
    {
        if (box3State)
        {
            box3State = false;
        }
        else
        {
            box3State = true;
        }
    }
}
