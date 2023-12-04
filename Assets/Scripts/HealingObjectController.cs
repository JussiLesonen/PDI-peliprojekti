using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealingObjectController : MonoBehaviour
{
    public int healingAmount = 5;

    [SerializeField] AudioSource HealingSound;

    [System.Obsolete]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!Player.isDead)
            {
                Player.AddBulletHits(-healingAmount);
            }

            gameObject.SetActive(false);
            HealingSound.Play();
        }
    }
}
