using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    private void Start()
    {
        optionsMenu.SetActive(false);
    }

    public void OptionsWindow()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsWindow()
    {
        optionsMenu.SetActive(false);
    }
}
