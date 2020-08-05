using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Get player
    public GameObject player;

    // Z-axis distance from camera to player
    float distance = -10f;

    // Used to create a Vector3 position, since you can't individually change the x, y, and z values of an objects position
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Set pos to player's position
        pos = player.transform.position;

        // Update distance
        pos.z += distance;

        // Change camera position to pos
        transform.position = pos;
        
    }
}
