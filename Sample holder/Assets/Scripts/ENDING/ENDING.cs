using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ENDING : MonoBehaviour
{

    PlayerControls controls;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 5);
    }

    public void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Start.started += ctx => PlayGame();
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

