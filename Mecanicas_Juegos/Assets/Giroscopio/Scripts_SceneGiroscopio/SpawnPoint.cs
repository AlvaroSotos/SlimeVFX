using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;
    public GameObject enemy;
    public int spawnPositionX;
    bool lastPlatformWithEnemy;
    GameController gameController;

    private void Start()
    {
        transform.position = new Vector3(spawnPositionX, 0, 55.6661f);
        gameController = FindObjectOfType<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        //distancia de spawn * numero de plataformas que quieres crear al principio 5.51661 * n (10)
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + 55.6661f);
    }

    public void SpawnPlatform(bool createEnemy, int numberPlatform)
    {

        GameObject newPlatform = Instantiate(gameController.platformPrefabs[numberPlatform], new Vector3(transform.position.x, transform.position.y, transform.position.z - .7f), Quaternion.identity);
        gameController.lastPlatformInstantiated = newPlatform;

        if(!lastPlatformWithEnemy)
        {
            if(createEnemy)
            {
                Instantiate(enemy, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Quaternion.identity);
                lastPlatformWithEnemy = true;
            }
           
        }
        else
        {
            lastPlatformWithEnemy = false;
        }
     
    }
}
