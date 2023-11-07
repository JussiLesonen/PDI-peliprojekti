using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static string color;

    public GameObject playerGraphics;

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
    }

    public void Customization()
    {
        SceneManager.LoadScene("Customization");
    }
}
