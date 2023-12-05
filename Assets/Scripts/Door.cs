using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Door : MonoBehaviour
{
    public Material material;

    float alpha = 1;

    private void Start()
    {
        material.color = Color.white;
    }

    void Update()
    {
        if (ItemCollector.hasKey && Objectives.timeValue > 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            alpha = Mathf.SmoothStep(alpha, 0.3f, Time.deltaTime * 10f);

            material.color = new Color(1, 1, 1, alpha);
        }
        else
        {  
            GetComponent<BoxCollider>().enabled = true;
            alpha = Mathf.SmoothStep(alpha, 1, Time.deltaTime * 20f);

            material.color = new Color(1, 1, 1, alpha);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (transform.name == "Door1")
            {
                Objectives.level = 2;
                GameObject.Find("ObjectivesText").GetComponent<TextMeshProUGUI>().text = "Objectives: Collect 5 coins";
            }
            if (transform.name == "Door2")
            {
                Objectives.level = 3;
                GameObject.Find("ObjectivesText").GetComponent<TextMeshProUGUI>().text = "Objectives: Collect 6 coins";
            }
            if (transform.name == "Door3")
            {
                Objectives.level = 4;
            }
            ItemCollector.hasKey = false;      
            //GameObject.Find("ObjectivesText").GetComponent<TextMeshProUGUI>().text="Objectives: Collect 5 coins";
            ItemCollector.coins = 0;
            ItemCollector.coinsText.text = "Coins: " + ItemCollector.coins;
            Objectives.canSpawn= true;
            Debug.Log(Objectives.canSpawn);
        }
    }
}
