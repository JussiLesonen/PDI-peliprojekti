using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverBar : MonoBehaviour
{
    public Slider slider;

    void Update()
    {
        slider.value = Player.floatTimer;
    }
}
