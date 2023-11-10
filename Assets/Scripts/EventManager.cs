using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static string color;

    public GameObject pauseMenu;
    public GameObject playerGraphics;

    public static bool isPaused = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (ColorButton.defaultColorChanged == false)
        {
            playerGraphics.GetComponent<Renderer>().material = Resources.Load("Default Player") as Material;
        }
        else
        {
            playerGraphics.GetComponent<Renderer>().material = Resources.Load(color) as Material;
        }

        if (!Player.isDead)
        {
            if (pauseMenu.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isPaused = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isPaused = false;
                }
            }
        }

        if (isPaused)
        {
            pauseMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);

            if (!Player.isDead)
            {
                Cursor.lockState = CursorLockMode.Locked;

                Time.timeScale = 1;
            }
        }
    }

    public void ResumeButton()
    {
        isPaused = false;
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene("Options");
    }

    public void QuitButton()
    {
        isPaused = false;

        SceneManager.LoadScene("Menu");
    }
}
