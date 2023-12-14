using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorButton : MonoBehaviour
{
    int ID;

    public static bool defaultColorChanged = false;

    public int requiredCoins;

    public GameObject Lock;

    bool isLocked = true;

    TextMeshProUGUI requiredAmount;

    void Start()
    {
        ID = GetInstanceID();

        if (name.Contains("Default"))
        {
            transform.Find("Checkmark").gameObject.SetActive(true);
        }

        requiredAmount = transform.Find("Lock").transform.Find("RequiredCoinsText").GetComponent<TextMeshProUGUI>();
        requiredAmount.text = requiredCoins.ToString();
    }

    private void Update()
    {
        if (Customization.totalCoins >= requiredCoins)
        {
            isLocked = false;
        }

        if (!isLocked)
        {
            Lock.SetActive(false);

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
        else
        {
            Lock.SetActive(true);
        }
    }

    public void SetColor()
    {
        if (!isLocked)
        {
            Customization.currentID = ID;

            defaultColorChanged = true;

            Debug.Log("Changed player color to " + name);

            EventManager.color = name;
        }
    }
}
