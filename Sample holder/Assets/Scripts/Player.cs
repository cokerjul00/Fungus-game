using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerControls controls;

    Vector3 move;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.right.performed += ctx => move = ctx.ReadValue<Vector3>();
        controls.Gameplay.right.canceled += ctx => move = Vector3.zero;



    }


    private void Update()
    {
        Vector3 m = new Vector3(-move.x, move.y) * Time.deltaTime;
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

