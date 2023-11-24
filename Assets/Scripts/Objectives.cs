using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objectives : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    public static bool canSpawn = true;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ItemCollector.coins + " " + canSpawn);

        if (ItemCollector.coins == 2 && canSpawn)
        {
            Instantiate(Resources.Load("Key"), transform.position,
            Quaternion.Euler(Vector3.zero));
            canSpawn = false;
            objectiveText.text = "Objectives completed";
        }
        else
        {
            objectiveText.text = "Collect 2 coins";
        }
    }
}
