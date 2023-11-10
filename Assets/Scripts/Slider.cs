using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hovering = false;

    private void Start()
    {
        PlayerPrefs.GetFloat(name);

        transform.localPosition = new Vector2(PlayerPrefs.GetFloat(name), 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(0))
        {
            hovering = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && hovering)
        {
            transform.position = Input.mousePosition;
        }

        if (!Input.GetMouseButton(0) && !hovering)
        {

        }

        transform.localPosition = new Vector2(Mathf.Clamp(transform.localPosition.x, -410, 410), 0);
        PlayerPrefs.SetFloat(name, transform.localPosition.x);

        Options.masterVolume = transform.localPosition.x;
    }
}
