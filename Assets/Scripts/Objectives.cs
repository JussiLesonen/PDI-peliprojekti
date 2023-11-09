using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objectives : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemCollector.coins == 2 && canSpawn)
        {
            Instantiate(Resources.Load("Coin"), transform.position, 
            Quaternion.Euler(Vector3.zero));
            canSpawn = false;
            objectiveText.text="Objectives: New objective";
        }
    }
}
