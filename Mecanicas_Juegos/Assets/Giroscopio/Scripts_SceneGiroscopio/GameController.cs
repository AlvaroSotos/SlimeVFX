using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] platformPrefabs;
    public GameObject slime;
    public GameObject lastPlatformInstantiated = null;
    bool firstPlatform = false;
    bool generateFromSpawn = false;

    public float distanceToSpawn = 4f;
    public int distanceFromPlatform = 0;



    [Header("Canvas")]
    public GameObject scoreText;
    public GameObject scoreFinalText;
    public GameObject counterStartText;
    public GameObject panelRestart;
    public int score = 0;
    public int counterTime = 3;

    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        slime = GameObject.FindGameObjectWithTag("Player");
        slime.transform.GetComponent<TouchInput>().enabled = false;
       
    }
    void Start()
    {
        InitGame();
    }

    public void InitGame()
    {
        score = 0;
        counterTime = 3;
        panelRestart.SetActive(false);

        GenerateFirstPlatforms();
        StartCounter();


    }
    
    public void EndGame()
    {
        panelRestart.SetActive(true);
        scoreFinalText.transform.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

   public void Restart()
    {
        SceneManager.LoadScene("Giroscopio_Movil");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void StartCounter()
    {
        StartCoroutine("CounterStart");
    }

    private void FixedUpdate()
    {

        distanceFromPlatform = 84 - (int)lastPlatformInstantiated.transform.position.z;

        if (distanceFromPlatform == distanceToSpawn)
        {
            //distanceToSpawn = (decimal)slime.GetComponent<PlayerJump>().maxHigh - .1m;
            print("Distancia desde la plataforma" + distanceFromPlatform);
            print("Distancia de Spawn" + distanceToSpawn);
            //GenerateNewPlatform();
        }
    }


   
    void GenerateFirstPlatforms()
    {
       
        if (!firstPlatform)
        {
            int randomPlatformPrefab = Random.Range(0, 3);
            lastPlatformInstantiated = Instantiate(platformPrefabs[randomPlatformPrefab], new Vector3(0, 0, 0), Quaternion.identity);
            lastPlatformInstantiated.transform.GetComponent<PlaneController>().enabled = false;
            firstPlatform = true;
        }

        while (lastPlatformInstantiated.transform.position.z <= 50)
        {
            int randomPlatformPrefab = Random.Range(0, 3);
            int randomPositionX = 1;//Random.Range(0, 3); // 
            switch (randomPositionX)
            {
                case 0:
                    GameObject newPlatform = Instantiate(platformPrefabs[randomPlatformPrefab],
                                                         new Vector3(-4, 0, lastPlatformInstantiated.transform.position.z + distanceToSpawn),
                                                         Quaternion.identity);

                    newPlatform.transform.GetComponent<PlaneController>().enabled = false;
                    lastPlatformInstantiated = newPlatform;

                    break;

                case 1:
                    newPlatform = Instantiate(platformPrefabs[randomPlatformPrefab],
                                              new Vector3(0, 0, lastPlatformInstantiated.transform.position.z + distanceToSpawn),
                                              Quaternion.identity);

                    newPlatform.transform.GetComponent<PlaneController>().enabled = false;
                    lastPlatformInstantiated = newPlatform;
                    break;

                case 2:
                    newPlatform = Instantiate(platformPrefabs[randomPlatformPrefab],
                                              new Vector3(4, 0, lastPlatformInstantiated.transform.position.z + distanceToSpawn),
                                              Quaternion.identity);

                    newPlatform.transform.GetComponent<PlaneController>().enabled = false;
                    lastPlatformInstantiated = newPlatform;
                    break;
            }


        }

    }

    public void GenerateNewPlatform()
    {
        int randomPositionX = 0; //Random.Range(0, 3); //
        int randomPlatformPrefab = Random.Range(0, 3);
        int spawnEnemy = Random.Range(0, 51);
        
        switch (randomPositionX)
        {
            case 0:             
                if (spawnEnemy <= 20)
                {
                    spawnPoints[0].transform.GetComponent<SpawnPoint>().SpawnPlatform(true, randomPlatformPrefab);
                }
                else
                {
                    spawnPoints[0].transform.GetComponent<SpawnPoint>().SpawnPlatform(false, randomPlatformPrefab);
                }
                break;

            case 1:             
                if (spawnEnemy <= 20)
                {
                    spawnPoints[1].transform.GetComponent<SpawnPoint>().SpawnPlatform(true, randomPlatformPrefab);
                }
                else
                {
                    spawnPoints[1].transform.GetComponent<SpawnPoint>().SpawnPlatform(false, randomPlatformPrefab);
                }
                break;

            case 2:              
                if (spawnEnemy <= 20)
                {
                    spawnPoints[2].transform.GetComponent<SpawnPoint>().SpawnPlatform(true, randomPlatformPrefab);
                }
                else
                {
                    spawnPoints[2].transform.GetComponent<SpawnPoint>().SpawnPlatform(false, randomPlatformPrefab);
                }
                break;
        }
    }

    public void IncreaseScore()
    {
        scoreText.transform.GetComponent<TextMeshProUGUI>().text = score.ToString();
        AnimateScore();
    }

    void AnimateScore()
    {
        // 1
        LeanTween.cancel(scoreText.gameObject);
        scoreText.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        scoreText.transform.localScale = Vector3.one;

        // 2
        LeanTween.rotateZ(scoreText.gameObject, 15.0f, 0.5f).setEasePunch();
        LeanTween.scaleX(scoreText.gameObject, 1.5f, 0.5f).setEasePunch();
    }

    IEnumerator CounterStart()
    {

      
        yield return new WaitForSeconds(1);

        if(counterTime != 0)
        {
            counterTime -= 1;
            counterStartText.transform.GetComponent<TextMeshProUGUI>().text = counterTime.ToString();
            counterStartText.transform.GetComponent<TextMeshProUGUI>().color = Color.black;
            // 1
            LeanTween.cancel(counterStartText.gameObject);
            counterStartText.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            counterStartText.transform.localScale = Vector3.one;




            // 2
            LeanTween.rotateZ(counterStartText.gameObject, 15.0f, 0.5f).setEasePunch();
            LeanTween.scaleX(counterStartText.gameObject, 1.5f, 0.5f).setEasePunch();
            StartCoroutine("CounterStart");
            StartCoroutine("ChangeColor");
        }
        else
        {    
            counterStartText.SetActive(false);
            slime.transform.GetComponent<TouchInput>().enabled = true;
            slime.GetComponent<Rigidbody>().isKinematic = false;
            StopCoroutine("CounterStart");
        }
        

    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(.15f);
        counterStartText.transform.GetComponent<TextMeshProUGUI>().color = Color.white;

    }
}

