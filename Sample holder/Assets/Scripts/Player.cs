using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce;


    PlayerControls controls;
    Vector2 move;


    private Rigidbody rb;

    
    bool jumpOn = true;


    public float speed = 5f;


    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Jump.performed += ctx => jump();



    }

    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag == "ground")
        {
            jumpOn = true;
        }
        
    }


    void jump()
    {
        if (jumpOn == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpOn = false;
        }
        else return;
    }

    private void Update()
    {
        Vector2 m = new Vector2(move.x, move.y) * speed * Time.deltaTime;
        transform.Translate(m, Space.World);
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }


    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }


 

}

