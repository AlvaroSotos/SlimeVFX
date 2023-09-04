using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool onSite = false;
    public float force;
    GameObject enemy;
    GameObject player;
    private void Start()
    {
        enemy = transform.GetChild(0).gameObject;
        StartCoroutine("Idle");
    }

    private void Update()
    {
        if(onSite)
        {
            transform.LookAt(player.transform);
        }
    }

    public void AttackPlayer(Transform target)
    {
        StopCoroutine("Idle");
        StartCoroutine(Attack(target));
        enemy.transform.localScale = new Vector3(1, 1, 1);
        LeanTween.scale(enemy, new Vector3(1.4f, 0.4f, 1.3f), 1);

        
    }

    IEnumerator Idle()
    {
        enemy.transform.localScale = new Vector3(1, 1, 1);
        LeanTween.scale(enemy, new Vector3(1.1f, 0.6f, 1), 1).setEase(LeanTweenType.punch);       
        yield return new WaitForSeconds(1);
        StartCoroutine("Idle");
    }

    IEnumerator Attack(Transform target)
    {      
        yield return new WaitForSeconds(1.5f);
        enemy.transform.localScale = new Vector3(1, 1, 1);
        Vector3 direction = transform.position - target.transform.position;
        gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.transform.GetComponent<Rigidbody>().AddForce(direction.normalized * force, ForceMode.VelocityChange);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            player = other.gameObject;
            onSite = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            onSite = false;
            player = null;
        }
    }
}
