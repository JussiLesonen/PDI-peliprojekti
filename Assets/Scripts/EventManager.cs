using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EventManager : MonoBehaviour
{
    public static string color;

    public GameObject pauseMenu;
    public GameObject playerGraphics;

    public static bool isPaused = false;

    public static int totalCoins;

    static bool resetSpawn = true;

    private void Start()
    {
        if (resetSpawn)
        {
            PlayerPrefs.SetInt("Respawn", 1);

            resetSpawn = false;
        }

        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            Options.masterVolume = 0;
        }
        else if (PlayerPrefs.GetInt("Mute") == 0)
        {
            Options.masterVolume = VolumeSlider.volume;
        }

        if (PlayerPrefs.GetInt("Respawn") == 2)
        {
            Objectives.level = 2;
            GameObject.Find("ObjectivesText").GetComponent<TextMeshProUGUI>().text = "Collect 5 coins";
        }
        if (PlayerPrefs.GetInt("Respawn") == 3)
        {
            Objectives.level = 3;
            GameObject.Find("ObjectivesText").GetComponent<TextMeshProUGUI>().text = "Collect 6 coins";
        }
        if (PlayerPrefs.GetInt("Respawn") == 4)
        {
            Objectives.level = 4;
            GameObject.Find("ObjectivesText").GetComponent<TextMeshProUGUI>().text = "Collect 8 coins";
        }
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

        ItemCollector.coins = 0;

        Objectives.canSpawn = true;
    }
}
