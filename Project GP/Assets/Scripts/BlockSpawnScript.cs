using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnScript : MonoBehaviour
{
    // Block Prefab
    public GameObject block;
    public GameObject bouncyBlock;
    public GameObject movingBlock;
    public GameObject finish;
    public GameObject shop;
    public GameObject shopDoor;
    GameObject prev;
    GameObject temp;
    public int numBlocks;
    int randomBlock;
    float randomX;
    float randomY;
    Vector3 tempPos;
    Vector3 newBlockPos;

    // Has a shop door yet
    bool shopDoorSpawned;

    // Stuff for moving platform
    MovingBlockScript mbScript;
    float randomDist;
    float randomSpeed;
    float movingBlockEndPos;

    // Player
    public GameObject player;
    Vector3 playerpos;

    // Spawning Enemies
    int spawnChance;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        shopDoorSpawned = false;

        // Spawn Shop somewhere far away
        Instantiate(shop, new Vector3(1000, 1000, 0), Quaternion.identity);

        // Get player position
        playerpos = player.transform.position;

        // Spawns in first platform underneath the player
        prev = Instantiate(block, playerpos - new Vector3(0, 3, 0), Quaternion.identity);

        // For loop to spawn in (currently) 10 blocks at a random x and y distance away from the last spawned block
        // The numbers randomly generated are relatively adjusted to the player's movement, so it should only create a new block that the player is able to get to from the last one
        // This is probably not perfect though
        for(int i = 0; i < numBlocks; i++)
        {
            // Get random x and y coordinates
            randomX = Random.Range(3f, 6f);
            randomY = Random.Range(-1.5f, 1.5f);
            tempPos = new Vector3(randomX, randomY, 0);
            // TempPos here is actually a random distance used to calculate a new distance further from the previous platform
            // Bad variable naming tbh but I'm too lazy to change it now

            // Calculate the new position of the block
            newBlockPos = prev.transform.position + new Vector3(prev.GetComponent<BoxCollider2D>().size.x, 0, 0) + tempPos + new Vector3(randomDist, 0, 0);


            // Get random block
            randomBlock = Random.Range(1, 8);

            // Spawn Bouncy Block
            if (randomBlock == 1)
            {
                // Spawn new block
                temp = Instantiate(bouncyBlock, newBlockPos, Quaternion.identity);

                randomDist = 0;
            }

            // Spawn Moving Block
            else if (randomBlock == 2)
            {
                temp = Instantiate(movingBlock, newBlockPos, Quaternion.identity);

                // Access script of moving block
                mbScript = temp.GetComponent<MovingBlockScript>();

                // Get random speed and distance the moving block will travel
                randomSpeed = Random.Range(2f, 4f);
                randomDist = Random.Range(3f, 6f);

                // Set values
                mbScript.moveDistance = randomDist;
                mbScript.moveSpeed = randomSpeed;
            }

            else
            {
                // Spawn new block
                temp = Instantiate(block, newBlockPos, Quaternion.identity);
                randomDist = 0;

                // 1 in 4 chance of spawning an shop door if there hasn't been one already
                spawnChance = Random.Range(1, 5);
                if (spawnChance == 1 && !shopDoorSpawned)
                {
                    Instantiate(shopDoor, newBlockPos + new Vector3(0, 1.8f, 0), Quaternion.identity);
                    shopDoorSpawned = true;
                }

            }

            // Set random size of block
            temp.transform.localScale = new Vector3(Random.Range(0.3f, 2f), 1, 1);
            prev = temp;
            tempPos = prev.transform.position;

            // 1 in 10 chance of spawning an enemy
            spawnChance = Random.Range(1, 11);
            if (spawnChance == 1)
            {
                Instantiate(enemy, tempPos + new Vector3(0, 1.5f, 0), Quaternion.identity);
            }
        }

        // Spawn in Finish
        Instantiate(finish, prev.transform.position + new Vector3(Random.Range(3f, 6f), Random.Range(-1.5f, 1.5f), 0), Quaternion.identity);
    }
}
