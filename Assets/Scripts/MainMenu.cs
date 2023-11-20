using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        #region volume
        if (PlayerPrefs.GetFloat("MasterVolumeSlider") > 328)
        {
            Options.masterVolume = 1;
        }
        else if (PlayerPrefs.GetFloat("MasterVolumeSlider") < 328 && PlayerPrefs.GetFloat("MasterVolumeSlider") > 246)
        {
            Options.masterVolume = 0.9f;
        }
        else if (PlayerPrefs.GetFloat("MasterVolumeSlider") < 246 && PlayerPrefs.GetFloat("MasterVolumeSlider") > 164)
        {
            Options.masterVolume = 0.8f;
        }
        else if (PlayerPrefs.GetFloat("MasterVolumeSlider") < 164 && PlayerPrefs.GetFloat("MasterVolumeSlider") > 82)
        {
            Options.masterVolume = 0.7f;
        }
        else if (PlayerPrefs.GetFloat("MasterVolumeSlider") < 82 && PlayerPrefs.GetFloat("MasterVolumeSlider") > 0)
        {
            Options.masterVolume = 0.6f;
        }
        else if (PlayerPrefs.GetFloat("MasterVolumeSlider") < 0 && PlayerPrefs.GetFloat("MasterVolumeSlider") > -82)
        {
            Options.masterVolume = 0.5f;
        }
        else if (PlayerPrefs.GetFloat("MasterVolumeSlider") < -82 && PlayerPrefs.GetFloat("MasterVolumeSlider") > -164)
        {
            Options.masterVolume = 0.4f;
        }
        else if (PlayerPrefs.GetFloat("MasterVolumeSlider") < -164 && PlayerPrefs.GetFloat("MasterVolumeSlider") > -246)
        {
            Options.masterVolume = 0.3f;
        }
        else if (PlayerPrefs.GetFloat("MasterVolumeSlider") < -246 && PlayerPrefs.GetFloat("MasterVolumeSlider") > -328)
        {
            Options.masterVolume = 0.2f;
        }
        else if (PlayerPrefs.GetFloat("MasterVolumeSlider") < -328 && PlayerPrefs.GetFloat("MasterVolumeSlider") >= -410)
        {
            Options.masterVolume = 0.1f;
        }
        #endregion
    }

    public void PlayGameButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGameButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene("Options");
    }

    public void CustomizationButton()
    {
        SceneManager.LoadScene("Customization");
    }
}
