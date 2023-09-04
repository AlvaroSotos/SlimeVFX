using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject player;
    public GameObject platformPrefab;
    public float platformSpacing = 2.0f;
    public float platformSpeed = 2.0f;
    public Transform[] spawnPoints;
    public int maxPlatforms = 10;

    private GameObject[] platforms; // Array de plataformas generadas
    private int platformIndex = 0; // Índice de la plataforma actualmente activa
    private float lastSpawnPositionZ;
    private float initialPlayerPositionX;

    private void Start()
    {
        lastSpawnPositionZ = player.transform.position.z;
        initialPlayerPositionX = player.transform.position.x;
        platforms = new GameObject[maxPlatforms];

        // Generar las primeras plataformas
        float spawnPositionZ = lastSpawnPositionZ + platformSpacing;
        for (int i = 0; i < maxPlatforms; i++)
        {
            platforms[i] = SpawnPlatform(spawnPositionZ);
            platforms[i].SetActive(false);
            spawnPositionZ += platformSpacing;
        }

        ActivateNextPlatform();
    }

    private void Update()
    {
        // Mover el jugador en el eje X cuando se rota la pantalla del móvil
        float playerRotation = Input.gyro.attitude.eulerAngles.z;
        player.transform.position = new Vector3(initialPlayerPositionX + playerRotation, player.transform.position.y, player.transform.position.z);

        float playerDistance = player.transform.position.z - lastSpawnPositionZ;

        if (playerDistance > platformSpacing)
        {
            if (playerRotation != 0) // Solo avanzar si el jugador no está en la posición inicial del eje X
            {
                lastSpawnPositionZ = player.transform.position.z;

                platforms[platformIndex].SetActive(false); // Desactivar la plataforma actual
                platforms[platformIndex] = SpawnPlatform(player.transform.position.z + platformSpacing); // Generar una nueva plataforma

                ActivateNextPlatform();
            }
            else // El jugador está en la posición inicial del eje X, reiniciar la posición de las plataformas
            {
                ResetPlatforms();
            }
        }

        MovePlatforms();
    }

    private GameObject SpawnPlatform(float spawnPositionZ)
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[randomIndex].position;
        spawnPosition.z = spawnPositionZ;
        return Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
    }

    private void MovePlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            if (platform.activeSelf)
            {
                platform.transform.Translate(Vector3.forward * platformSpeed * Time.deltaTime);
            }
        }
    }

    private void ActivateNextPlatform()
    {
        platformIndex++;
        if (platformIndex >= maxPlatforms)
        {
            platformIndex = 0;
        }

        platforms[platformIndex].SetActive(true);
    }

    private void ResetPlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false);
        }

        float spawnPositionZ = lastSpawnPositionZ + platformSpacing;
        for (int i = 0; i < maxPlatforms; i++)
        {
            platforms[i] = SpawnPlatform(spawnPositionZ);
            platforms[i].SetActive(false);
            spawnPositionZ += platformSpacing;
        }

        ActivateNextPlatform();
    }
}
