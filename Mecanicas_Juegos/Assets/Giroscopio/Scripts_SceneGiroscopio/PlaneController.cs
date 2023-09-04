using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speedMovement;


    private Camera camera;
    public Rigidbody rb;
    public GameObject playerRigidBody;
    private BoxCollider objectCollider;
    Plane[] cameraFrustum;

    Transform[] spawnPoints;
    public float aceleracion = 0.458f;


    private void Start()
    {

        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        playerRigidBody = GameObject.FindGameObjectWithTag("Player");
        objectCollider = GetComponent<BoxCollider>();
    }
    private void FixedUpdate()
    {
        //if(playerRigidBody.transform.GetComponent<Rigidbody>().velocity.y >= 0)
        //{
        //    Vector3 direccion = new Vector3(0f, 0 , -aceleracion * speedMovement);
        //    rb.AddForce(direccion, ForceMode.Acceleration);
        //}
        //else
        //{
        //    Vector3 direccion = new Vector3(0f, 0f, aceleracion * speedMovement);
        //    rb.AddForce(direccion, ForceMode.Acceleration);
        //}
       
        //transform.Translate(Vector3.back * speedMovement * Time.deltaTime);

        var bounds = objectCollider.bounds;

        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);
        if (!GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            Destroy(gameObject);
        }

    }


}
