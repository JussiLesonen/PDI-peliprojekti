using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Customization : MonoBehaviour
{
    public static int currentID;

    public TextMeshProUGUI coinsText;

    public static int totalCoins;

    private void Update()
    {
        coinsText.text = totalCoins.ToString();
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
