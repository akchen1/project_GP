using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    // Bullet game object
    public GameObject bullet;
    // Position where the bullet appears from
    public Transform shotPoint;

    // Number of bullets before needing to reload
    public int maxBullets;
    // Number of bullets left
    private int currentBullets;

    // Time it takes to reload
    public float reloadSpeed;
    // This variable is how fast the player can shoot (ie. if shootSpeed is set to 1, player can shoot once every 1 second)
    public float shootSpeed;
    // Weapon recoil (knockback)
    public float recoil;

    // Variables to keep track of how fast the player can reload
    float reloadTimer;
    // Variables to keep track of how fast the player can shoot
    float shootTimer;
    
    // Boolean to keep track if player can shoot or not (True means can shoot)
    private bool canShoot;
    private bool isReload;

    // Start is called before the first frame update
    void Start()
    {
        currentBullets = maxBullets;
        isReload = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if shoot timer is above 0, meaning the player can't shoot
        shootTimer = shootTimer > 0f ? shootTimer - Time.deltaTime : 0f;

        // Check if reload timer is above 0, meaning player is reloading
        //reloadTimer = reloadTimer > 0f ? reloadTimer - Time.deltaTime : 0f;

        // Weapon can shoot if number of bullets is greater than zero, it is not reloading, and it is not shooting
        canShoot = currentBullets > 0 && reloadTimer <= 0f && shootTimer == 0f ? true : false;

      
        // Check if reload timer is above 0, meaning player is reloading
        reloadTimer = reloadTimer > 0f ? reloadTimer - Time.deltaTime : 0f;
        
        if (isReload && reloadTimer <= 0f)
        {
            isReload = false;
            currentBullets = maxBullets;
        }
    }

    // Shoots the weapon
    public bool Shoot()
    {
        if (canShoot)
        {
            Instantiate(bullet, shotPoint.position, Quaternion.identity);
            currentBullets--;
            shootTimer = shootSpeed;
        }
        return canShoot;
 
    }

    // Reloads the weapon
    public void Reload()
    {
        if (currentBullets == maxBullets)
        {
            return;
        }
        isReload = true;
        reloadTimer = reloadSpeed;
    }

    public int getCurrentBullets()
    {
        return currentBullets;
    }

    public bool getIsReload()
    {
        return isReload;
    }
}
