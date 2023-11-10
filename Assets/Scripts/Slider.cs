using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hovering = false;
    bool holding = false;

    float volume;

    private void Start()
    {
        PlayerPrefs.GetFloat(name);

        transform.localPosition = new Vector2(PlayerPrefs.GetFloat(name), 0);

        volume = transform.localPosition.x;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }

    void Update()
    {
        if (hovering)
        {
            if (Input.GetMouseButton(0))
            {
                holding = true;
            }
        }

        if (!Input.GetMouseButton(0))
        {
            holding = false;
        }

        if (holding)
        {
            transform.position = Input.mousePosition;
        }

        transform.localPosition = new Vector2(Mathf.Clamp(transform.localPosition.x, -410, 410), 0);
        PlayerPrefs.SetFloat(name, transform.localPosition.x);

        if (transform.localPosition.x > 328)
        {
            volume = 1;
        }
        else if (transform.localPosition.x < 328 && transform.localPosition.x > 246)
        {
            volume = 0.9f;
        }
        else if (transform.localPosition.x < 246 && transform.localPosition.x > 164)
        {
            volume = 0.8f;
        }
        else if (transform.localPosition.x < 164 && transform.localPosition.x > 82)
        {
            volume = 0.7f;
        }
        else if (transform.localPosition.x < 82 && transform.localPosition.x > 0)
        {
            volume = 0.6f;
        }
        else if (transform.localPosition.x < 0 && transform.localPosition.x > -82)
        {
            volume = 0.5f;
        }
        else if (transform.localPosition.x < -82 && transform.localPosition.x > -164)
        {
            volume = 0.4f;
        }
        else if (transform.localPosition.x < -164 && transform.localPosition.x > -246)
        {
            volume = 0.3f;
        }
        else if (transform.localPosition.x < -246 && transform.localPosition.x > -328)
        {
            volume = 0.2f;
        }
        else if (transform.localPosition.x < -328 && transform.localPosition.x >= -410)
        {
            volume = 0.1f;
        }

        Options.masterVolume = volume;
    }
}
