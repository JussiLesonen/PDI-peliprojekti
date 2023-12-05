using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    int ID;

    public static bool defaultColorChanged = false;

    public int requiredCoins;

    void Start()
    {
        ID = GetInstanceID();

        if (name.Contains("Default"))
        {
            transform.Find("Checkmark").gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (defaultColorChanged == true)
        {
            if (EventManager.color == name)
            {
                transform.Find("Checkmark").gameObject.SetActive(true);
            }
            else
            {
                transform.Find("Checkmark").gameObject.SetActive(false);
            }
        }
    }

    public void SetColor()
    {
        Customization.currentID = ID;

        defaultColorChanged = true;

        Debug.Log("Changed player color to " + name);

        EventManager.color = name;
    }
}
