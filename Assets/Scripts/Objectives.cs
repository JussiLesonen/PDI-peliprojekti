using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objectives : MonoBehaviour
{
    TextMeshProUGUI objectiveText;
    public static bool canSpawn = true;
    public static float timeValue = 0;
    public static int level = 1;
    bool timeStarted = false;
    private void Start()
    {
        objectiveText = GameObject.Find("ObjectivesText").GetComponent<TextMeshProUGUI>();
    }
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
            objectiveText.text = "Objective: Collect the key";
        }
        if (ItemCollector.hasKey)
        {
            if (!timeStarted)
            {
                timeStarted = true;
            }
            if (timeValue < 0.1f && GameObject.Find("Key(Clone)") == null)
            {
                Instantiate(Resources.Load("Key"), new Vector3(-0.06f, 0.96f, 30.64f),
                Quaternion.Euler(Vector3.zero));
                Debug.Log(Resources.Load("Key(Clone)"));
                Debug.Log(timeValue);
                Debug.Log(transform.position);
            }
            objectiveText.text = "Go to the next stage within " + Mathf.Round(timeValue).ToString() + "s";
        }
        if (timeStarted && timeValue < 0.1f && level == 1)
        {
            objectiveText.text = "Objective: Collect the key";
        }
    }
}
