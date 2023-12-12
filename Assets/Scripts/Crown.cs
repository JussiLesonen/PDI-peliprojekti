using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    public GameObject winMenu;

    public static bool endGame = false;

    private void Start()
    {
        endGame = false;
    }

    void FixedUpdate()
    {
        if (Time.timeScale > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 1) * 0.007f, transform.position.z);

            transform.Rotate(0, 1, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log("You're winner!");

            winMenu.SetActive(true);

            endGame = true;

            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
        }
    }
}
