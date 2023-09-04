using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public EnemyScript enemyScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            enemyScript.AttackPlayer(other.transform);
        }
    }
}
