using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerControls controls;

    Vector2 move;

    public float speed = 5f;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => move = Vector2.zero;



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

