using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objectives : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    bool canSpawn = true;
    public static float timeValue = 0;
    bool timeStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
            objectiveText.text = ("Time: " + Mathf.Round(timeValue).ToString());
        }
       
    }

   
}
