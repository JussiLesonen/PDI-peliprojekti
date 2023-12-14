using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealingObjectController : MonoBehaviour
{
    public int healingAmount = 5;

    [SerializeField] AudioSource HealingSound;

    void FixedUpdate()
    {
        if (Time.timeScale > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 3) * 0.007f, transform.position.z);

            transform.Rotate(0, 1, 0);
        }
    }

    [System.Obsolete]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!Player.isDead)
            {
                Player.AddBulletHits(-healingAmount);
            }

            HealingSound.volume = Options.masterVolume;
            HealingSound.Play();

            transform.Find("Heart").GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Particle System").GetComponent<ParticleSystem>().Stop();

            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
