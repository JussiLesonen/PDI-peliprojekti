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
        canSpawn = true;
        level= 1;
    }

    void FixedUpdate()
    {
        if (timeValue <= 0)
        {
            timeValue = 0;

            ItemCollector.hasKey = false;
        }

        timeValue -= Time.deltaTime;

        if (ItemCollector.coins == 2 && canSpawn && level == 1)
        {
            //Instantiate(Resources.Load("Key"), transform.position,
            //Quaternion.Euler(Vector3.zero));
            canSpawn = false;
            objectiveText.text = "Collect the key";
            //debug
            Instantiate(Resources.Load("Key"), new Vector3(-0.06f, 0.96f, 30.64f),
            Quaternion.Euler(Vector3.zero));
        }
        else if (ItemCollector.coins == 5 && level == 2 && canSpawn)
        {
            Instantiate(Resources.Load("Key"), new Vector3(147.278168f, 20.5699997f, 30.2299995f),
            Quaternion.Euler(Vector3.zero));
            canSpawn = false;
            objectiveText.text = "Collect the key";
            Debug.Log(canSpawn);
        }
        else if (ItemCollector.coins == 6 && level == 3 && canSpawn)
        {
            Instantiate(Resources.Load("Key"), new Vector3(137.699997f, 27.1299992f, 214.429993f),
            Quaternion.Euler(Vector3.zero));
            canSpawn = false;
            objectiveText.text = "Collect the key";
            Debug.Log(canSpawn);
        }
        else if (ItemCollector.coins == 8 && level == 4)
        {
            //Instantiate(Resources.Load("Key"), new Vector3(126.959999f, 107.480003f, 496.119995f),
            //Quaternion.Euler(Vector3.zero));
            //canSpawn = false;
            objectiveText.text = "Collect the crown";
            Debug.Log(canSpawn);
        }

        if (ItemCollector.hasKey)
        {
            if (!timeStarted)
            {
                timeStarted = true;
            }
            if (timeValue < 0.1f && GameObject.Find("Key(Clone)") == null)
            {
                if (level == 1)
                {
                    Instantiate(Resources.Load("Key"), new Vector3(-0.06f, 0.96f, 30.64f),
                    Quaternion.Euler(Vector3.zero));
                }
                else if (level == 2)
                {
                    Instantiate(Resources.Load("Key"), new Vector3(147.278168f, 20.5699997f, 30.2299995f),
                    Quaternion.Euler(Vector3.zero));
                    Debug.Log(Resources.Load("Key(Clone)"));
                    Debug.Log(timeValue);
                    Debug.Log(transform.position);
                }
                else if (level == 3)
                {
                    Instantiate(Resources.Load("Key"), new Vector3(137.699997f, 27.1299992f, 214.429993f),
                    Quaternion.Euler(Vector3.zero));
                }
            }
            objectiveText.text = "Go to the next stage within " + Mathf.Round(timeValue).ToString();
        }
        if (timeStarted && timeValue < 0.1f && !canSpawn)
        {
            objectiveText.text = "Collect the key";
        }
    }
}
