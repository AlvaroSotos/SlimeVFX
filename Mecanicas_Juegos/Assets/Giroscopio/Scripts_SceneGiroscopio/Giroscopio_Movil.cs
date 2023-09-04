using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giroscopio_Movil : MonoBehaviour
{
    [SerializeField]
    float speed = 15f; 
    public Camera camera;
    public BoxCollider objectCollider;
    Plane[] cameraFrustum;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait; //Impide que la pantalla rote durante el juego

        camera = Camera.main;
        objectCollider = GetComponent<BoxCollider>();


    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direccion = Vector3.zero; //Establece la variable a 0
        direccion.x = (Mathf.Abs(Input.acceleration.x) > 0.1) ? Input.acceleration.x : 0; //Asigna la rotacion del movil al eje X
        if(direccion.sqrMagnitude > 1) direccion.Normalize(); //Ajusta el valor del vector para que no exceda sus valores

        direccion *= Time.deltaTime; //multiplica el vector por el tiempo
        transform.Translate(direccion *  speed); //Mueve el objeto

        //CODIGO PARA DETECTAR SI EL OBJETO SALE DE CAMARA
        
        var bounds = objectCollider.bounds; //variable para guardar el tamaño del collider
        //Condición para detectar si el objeto sale de los márgenes de la cámara
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);
        if(!GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            //Si el objeto está a la derecha y sale de los márgenes de la cámara aparecerá por la izq.
            if (transform.position.x > 1) 
            {
                transform.position = new Vector3(-transform.position.x + 1, transform.position.y, transform.position.z);
            }
            else  //Si el objeto está a la izq y sale de los márgenes de la cámara aparecerá por la derecha.
            {
                transform.position = new Vector3(-transform.position.x - 1, transform.position.y, transform.position.z);
            }
        }


    }
    
}


