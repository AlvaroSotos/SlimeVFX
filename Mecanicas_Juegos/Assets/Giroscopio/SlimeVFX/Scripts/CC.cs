using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC : MonoBehaviour
{
    public PlayerInputActions player_controls;

    Rigidbody rb;
    private Vector3 move_direction;
    private Vector2 movement;
    public float speed = 5f;
    public float jumpForce= 5f;
    public float gravity = -0.5f;
    public bool jumped;
    public bool isGrounded = true;

    public ParticleSystem jumpSquash;
    private void Awake()
    {
        jumpSquash = new ParticleSystem();
        player_controls = new PlayerInputActions();
        player_controls.Player.Move.performed += move_performed =>
        {
            movement = move_performed.ReadValue<Vector2>();
        };
        player_controls.Player.Move.canceled += move_canceled =>
        {
            movement = move_canceled.ReadValue<Vector2>();
        };
        player_controls.Player.Jump.performed += jump_performed =>
        {
            jumped = jump_performed.ReadValueAsButton();
        };
        player_controls.Player.Jump.canceled += jump_canceled =>
        {
            jumped = jump_canceled.ReadValueAsButton();
        };
    }
    private void OnEnable()
    {
        player_controls.Enable();
    }
    private void OnDisable()
    {
        player_controls.Disable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (movement != Vector2.zero)
        {
            move_direction = new Vector3(movement.x, 0.0f, movement.y);
            rb.velocity = move_direction * speed;
        }


        if (jumped && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }
        else
        {
            rb.AddForce(Vector3.down * gravity);

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == ("Plane"))
        {
            isGrounded = true;
            jumpSquash = GetComponentInChildren<ParticleSystem>();
            jumpSquash.Play();
        }
    }
}
