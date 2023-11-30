using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPowerup : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Time.timeScale > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 3) * 0.007f, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            GetComponent<MeshRenderer>().enabled = false;

            transform.Find("Particle System").GetComponent<ParticleSystem>().Stop();

            StartCoroutine(DestroyGameobject());
        }
    }

    IEnumerator DestroyGameobject()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Player.canUseHover = true;
    }
}
