using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{


    PlayerControls controls;
    Vector2 move;


    private Rigidbody rb;


    public float speed = 5f;
    public int Health;
    public int maxHealth = 100;




    private void Start()
    {
        Health = maxHealth;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spore")
        {
            Health -= 8;
        }
    }


    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => move = Vector2.zero;




    }


    private void Update()
    {

        if (Health <= 0)
        {

            Destroy(gameObject);
            
        }


        

        Vector2 m = new Vector2(move.x, move.y) * speed * Time.deltaTime;
        transform.Translate(m, Space.World);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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

