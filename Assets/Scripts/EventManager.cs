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
        playerGraphics.GetComponent<Renderer>().material = Resources.Load(color) as Material;
    }

    public void Customization()
    {
        SceneManager.LoadScene("Customization");
    }
}
