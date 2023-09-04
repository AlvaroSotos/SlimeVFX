using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public GameObject ball;
    public Transform spawnPoint;

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                Instantiate(ball, spawnPoint.position, Quaternion.identity);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(ball, spawnPoint.position, Quaternion.identity);
        }

    }
}
