using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public static float masterVolume;

    public void BackButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
