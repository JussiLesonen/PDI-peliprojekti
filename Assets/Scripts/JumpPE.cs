using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPE : MonoBehaviour
{
    public ParticleSystem jumpPE;
    public AudioClip thud;

    float jumpPECooldown;

    void Update()
    {
        jumpPECooldown -= Time.deltaTime;

        if (jumpPECooldown <= 0)
        {
            jumpPECooldown = 0;
        }

        gameObject.GetComponent<AudioSource>().volume = Options.masterVolume;
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6f && jumpPECooldown < 0.1f)
        {
            jumpPECooldown = 0.3f;

            jumpPE.Play();
            Debug.Log(jumpPECooldown);

            gameObject.GetComponent<AudioSource>().pitch = Random.RandomRange(7, 9);
            gameObject.GetComponent<AudioSource>().PlayOneShot(thud);
        }
    }
}
