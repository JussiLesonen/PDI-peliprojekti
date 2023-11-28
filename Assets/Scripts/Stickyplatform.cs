using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickyplatform : MonoBehaviour
{
    public bool onPlatform = false;

    private void Update()
    {
        if (onPlatform)
        {
            GameObject.Find("Player").transform.SetParent(transform);
        }

        if (Vector3.Distance(transform.position, GameObject.Find("GroundCheck").transform.position) > transform.localScale.x / 2 && transform.Find("Player"))
        {
            GameObject.Find("Player").transform.SetParent(null);

            onPlatform = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GroundCheck")
        {
            onPlatform = true;
        }
    }
}
