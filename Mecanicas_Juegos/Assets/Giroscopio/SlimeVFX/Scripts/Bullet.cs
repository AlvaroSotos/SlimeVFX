using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
        //StartCoroutine(BulletTimeDestroy());
    }
    IEnumerator BulletTimeDestroy()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
        Destroy(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
