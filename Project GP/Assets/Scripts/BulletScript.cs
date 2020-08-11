using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Rigidbody variable used to create constant horizontal movement
    Rigidbody2D rbody;

    // Direction to shoot
    Vector3 shootDirection;

    // Speed of bullet
    int bulletSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        // Find rigidbody
        rbody = GetComponent<Rigidbody2D>();

        // Get direction
        shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        shootDirection.z = 0;
        shootDirection.Normalize();

        // Set velocity to shoot bullet
        rbody.velocity = shootDirection * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This is a built in unity function that checks when the object collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the tag of the object it collides with is "Ground"
        if (collision.tag == "Ground" || collision.tag == "MovingPlatform")
        {
            // Destroy this specific instance of the bullet
            // Make sure to use this specific way of destroying instances of a prefab
            // If done incorrectly it could destroy the entire prefab, meaning the game wouldn't be able to spawn in any more bullets until the game restarts
            Destroy(this.gameObject);
        }

        // Check if bullet hits enemy
        else if (collision.tag == "Enemy")
        {
            // Access script of individual game object
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();

            // Subtract health
            enemyScript.health -= 1;

            // Destroy this bullet
            Destroy(this.gameObject);
        }
    }

    // This is also a built in unity function that checks if the object is no longer visible by any camera
    // Using this, we will delete the bullet when it is off-screen, to make sure the game doesn't lag with a shit ton of bullets
    // If you're testing the game without the game being maximized on play, the scene preview window will count as a camera
    private void OnBecameInvisible()
    {
        // Destroy this specific instance of the bullet
        Destroy(this.gameObject);
    }
}
