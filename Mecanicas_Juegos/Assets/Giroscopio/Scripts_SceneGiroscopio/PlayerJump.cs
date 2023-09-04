using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public GameObject slime;
    public PlaneController[] firstPlatforms;
    GameController gameController;
    public float jumpForce = 10;
    public float speedForce = 0;
   
    public bool jump = false;
    public bool jumping = false;
    public float maxHigh = 0f;
    public float cronometro = 2.3f;
    public float playerMaxVelocity = 0f;
    public float playerZdistance = 0f;
    public bool playerMoving = false;
    public bool firstJump = true;

    private void Start()
    {
        slime = transform.GetChild(0).gameObject;

        firstPlatforms = FindObjectsOfType<PlaneController>();

        gameController = FindObjectOfType<GameController>();


    }

    private void FixedUpdate()
    {
        if (jump)
        {

            jumping = true;

        }

        if (transform.position.y > maxHigh)
        {
            maxHigh = transform.position.y;
        }

        if(transform.GetComponent<Rigidbody>().velocity.y > playerMaxVelocity)
        {
            playerMaxVelocity = transform.GetComponent<Rigidbody>().velocity.y;
        }

        if(jumping)
        {
            gameController.score += 1;
            gameController.IncreaseScore();
        }

        if(!playerMoving)
        {
            playerZdistance = transform.position.z;
        }
       
      
    }

    void Jump()
    {
        gameObject.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 6, 0);
        gameObject.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        //Nueva Prueba
        gameObject.transform.GetComponent<Rigidbody>().AddForce(Vector3.forward * speedForce, ForceMode.VelocityChange);

        SpawnPlatform();
        ActivateCoroutine();
    }

    void SpawnPlatform()
    {
        if(!firstJump)
        {
            gameController.GenerateNewPlatform();
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            Jump();
            LeanTween.scale(gameObject, new Vector3(1.4f, 0.3f, 1.4f), 1.2f).setEase(LeanTweenType.punch);
            jump = true;

            if(!playerMoving)
            {
                playerMoving = true;
            }
            else
            {
                playerMoving = false;
            }
            
            while (firstPlatforms.Length != 0)
            {
                for (int i = 0; i < firstPlatforms.Length; i++)
                {
                    firstPlatforms[i].enabled = true;
                }
                Array.Resize(ref firstPlatforms, 0);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
           
            gameObject.transform.localScale = new Vector3(1, 1, 1);          
            jump = false;

            
        }
    }

    void ActivateCoroutine()
    {
        StartCoroutine("IncreaseScore");
    }

    IEnumerator IncreaseScore()
    {
        yield return new WaitForSeconds(1.15f);
       // SpawnPlatform();
        firstJump = false;
        jumping = false;
    }
}
