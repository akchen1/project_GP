using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnScript : MonoBehaviour
{
    // Block Prefab
    public GameObject block;
    public GameObject finish;
    GameObject prev;
    GameObject temp;
    public int numBlocks;
    float randomX;
    float randomY;
    Vector3 tempPos;
    Vector3 newBlockPos;

    // Player
    public GameObject player;
    Vector3 playerpos;

    // Start is called before the first frame update
    void Start()
    {
        // Get player position
        playerpos = player.transform.position;

        // Spawns in first platform underneath the player
        prev = Instantiate(block, playerpos - new Vector3(0, 3, 0), Quaternion.identity);

        // For loop to spawn in (currently) 10 blocks at a random x and y distance away from the last spawned block
        // The numbers randomly generated are relatively adjusted to the player's movement, so it should only create a new block that the player is able to get to from the last one
        // This is probably not perfect though
        for(int i = 0; i < numBlocks; i++)
        {
            randomX = Random.Range(3f, 6f);
            randomY = Random.Range(-1.5f, 1.5f);
            tempPos = new Vector3(randomX, randomY, 0);

            newBlockPos = prev.transform.position + tempPos;

            temp = Instantiate(block, newBlockPos, Quaternion.identity);
            temp.transform.localScale = new Vector3(Random.Range(0.1f, 2f), 1, 1);
            prev = temp;
            tempPos = prev.transform.position;
        }

        // Spawn in Finish
        Instantiate(finish, prev.transform.position + new Vector3(Random.Range(3f, 6f), Random.Range(-1.5f, 1.5f), 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
