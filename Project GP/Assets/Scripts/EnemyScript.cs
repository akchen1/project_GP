using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Rigidbody and BoxCollider variables
    Rigidbody2D rbody;
    BoxCollider2D coll;

    // Enemy Stats
    public int health;

    public int moneyValue;

    // LayerMask for the ground
    public LayerMask groundLayer;

    public bool canShoot;
    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        // Get rigidbody and collider
        rbody = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        // Initialize stats
        health = 3;
        moneyValue = 5;

    }

    // Update is called once per frame
    void Update()
    {

        // If health reaches 0
        if (health <= 0)
        {
            // Kill enemy
            GameObject.FindGameObjectWithTag("Currency").GetComponent<Currency>().updateGold(moneyValue);
            Destroy(this.gameObject);
        }

        // This checks if the enemy spawned on a moving platform so it can move with it
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, coll.bounds.size.y / 1.8f, groundLayer);

        // If it collides with something that isn't NULL
        if (hit.collider != null)
        {
            if (hit.collider.tag == "MovingPlatform")
            {
                rbody.velocity = new Vector2(hit.collider.GetComponent<Rigidbody2D>().velocity.x, 0);
            }
        }

        if (canShoot)
        {
            weapon.Shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            playerScript.Hit(1);
        }
    }
}
