using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    [System.Obsolete]
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger: " + gameObject.name);

        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player.AddBulletHits(1);
            Debug.Log("Bullet hits player");
            

            Destroy(gameObject);
        }
    }
}
