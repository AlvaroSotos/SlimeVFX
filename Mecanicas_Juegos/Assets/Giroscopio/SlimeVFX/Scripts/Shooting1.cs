using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting1 : MonoBehaviour
{
    public GameObject bullet;
    public GameObject pistola;
    public PlayerInputActions player_controls;

    public float cd_time = 0.2f;
    public float Next_bullet;


    public bool isShooting;

    public float bullet_speed;
    void Start()
    {
        
        
    }

    private void Awake()
    {

        //pool = GetComponent<Adaptative_pool>();

        player_controls = new PlayerInputActions();
        player_controls.Player.Shot.performed += shot_performed =>
        {
            isShooting = shot_performed.ReadValueAsButton();
        };
        player_controls.Player.Shot.canceled += shot_canceled =>
        {
            isShooting = shot_canceled.ReadValueAsButton();
        };
    }


        
       
    
    void Update()
    {
        if (isShooting && Time.time > Next_bullet)
        {
            Next_bullet = Time.time + cd_time;

            GameObject clone = Instantiate(bullet);
            clone.SetActive(true);
            clone.transform.position = transform.position;
            clone.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 1.0f, 1.0f) * bullet_speed, ForceMode.VelocityChange);
            
        }
    }

    private void OnEnable()
    {
        player_controls.Enable();
    }

    private void OnDisable()
    {
        player_controls.Disable();

    }
}
