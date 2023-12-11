using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
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
