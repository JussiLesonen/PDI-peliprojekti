using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickyplatform : MonoBehaviour
{
    bool onPlatform;

    private void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.Find("GroundCheck").transform.position) > transform.localScale.x)
        {
            GameObject.Find("Player").transform.SetParent(null);

            Debug.Log("Player left the platform");

            onPlatform = false;
        }

        if (onPlatform)
        {
            Debug.Log("Distance: " + Vector3.Distance(transform.position, GameObject.Find("GroundCheck").transform.position) + " / " + transform.localScale.x);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GroundCheck")
        {
            GameObject.Find("Player").transform.SetParent(transform);

            onPlatform = true;

            //Debug.Log(Vector3.Distance(transform.position, GameObject.Find("Player").transform.localPosition) + " / " + transform.localScale.x);
            //Debug.Log(transform.position);
        }
    }
}
