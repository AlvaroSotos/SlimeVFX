using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    GameController gC;

    private void Start()
    {
        gC = FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            gC.EndGame();
        }

        if(other.transform.CompareTag("Ball") && this.transform.name == "Enemy")
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
