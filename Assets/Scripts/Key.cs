using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        if (Objectives.level == 1)
        {
            Objectives.timeValue = 40;
        }
        else if(Objectives.level == 2)
        {
            Objectives.timeValue = 40;
        }
        else if(Objectives.level == 3)
        {
            Objectives.timeValue = 40;
        }
        else if (Objectives.level == 4)
        {
            Objectives.timeValue = 40;
        }

    }
}
