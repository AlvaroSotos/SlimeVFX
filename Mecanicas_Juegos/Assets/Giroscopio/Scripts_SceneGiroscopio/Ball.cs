using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public int force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
    }
}


