using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickyplatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GroundCheck")
        {
            GameObject.Find("Player").transform.SetParent(transform);
            
        }
    }

    private void OnTriggerEXit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").transform.SetParent(null);
        }
    }
}
