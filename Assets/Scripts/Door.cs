using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (ItemCollector.hasKey)
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
            ItemCollector.hasKey = false;
        }
    }
}
