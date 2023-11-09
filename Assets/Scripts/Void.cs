using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Void : MonoBehaviour
{
    public GameObject endMenu;

    float timer;

    private void Start()
    {
        timer = 3f;
    }

    private void Update()
    {
        if (Player.isDead)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 3f;
        }

        if (timer < 0.1f)
        {
            Time.timeScale = 0;

            endMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;

            Debug.Log("Paused");
        }

        if (timer < 0)
        {
            timer = 0;
        }

        //Debug.Log(timer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Player.isDead = true;
        }
    }

    public void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);

        Player.isDead = false;

        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");

        Player.isDead = false;

        Time.timeScale = 1;
    }
}
