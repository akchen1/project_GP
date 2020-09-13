using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoundariesScript : MonoBehaviour
{
    public EnemyScript eScript;

    // Start is called before the first frame update
    void Start()
    {
        eScript = GetComponentInParent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            eScript.canShoot = true;
        }
    }
}
