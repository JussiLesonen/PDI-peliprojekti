using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Customization : MonoBehaviour
{
    public static int currentID;

    public void Back()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
