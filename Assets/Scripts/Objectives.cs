using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objectives : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    public static bool canSpawn = true;
    public static float timeValue = 0;
    bool timeStarted = false;

    void Update()
    {
        if (timeValue <= 0)
        {
            timeValue = 0;
        }

        timeValue -= Time.deltaTime;

        if (ItemCollector.coins == 2 && canSpawn)
        {
            Instantiate(Resources.Load("Key"), transform.position, 
            Quaternion.Euler(Vector3.zero));
            canSpawn = false;
            objectiveText.text="Objective: Collect the key";
        }
        if (ItemCollector.hasKey)
        {
            if (!timeStarted)
            {
                timeValue += 15;
                timeStarted = true;
            }
            objectiveText.text = "Go to the next stage before " + Mathf.Round(timeValue).ToString() + "s";
        }
    }
}
