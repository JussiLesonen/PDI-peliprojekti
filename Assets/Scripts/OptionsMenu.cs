using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    public void OptionsWindow()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsWindow()
    {
        optionsMenu.SetActive(false);
    }
}
