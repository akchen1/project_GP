using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabDoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject doorStop;
    public bool open { get; set; }
    void Start()
    {
        open = false;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("DoorStop"))
        {
            open = false;
        }
    }

   
}
