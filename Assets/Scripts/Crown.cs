using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    public static bool endGame = false;

    new AudioSource audio;

    Vector3 startPos;

    private void Start()
    {
        endGame = false;

        startPos = transform.position;

        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (Time.timeScale > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 1) * 0.007f, transform.position.z);

            transform.Rotate(0, 1, 0);
        }
    }

    private void Update()
    {
        audio.volume = Options.masterVolume;

        var player = GameObject.Find("Player");

        if (endGame)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);

            transform.localScale = new Vector3(50f, 50f, 50f);

            var sparkle = transform.Find("SparklePE");

            sparkle.transform.localScale = new Vector3(0.06871825f, 0.06871825f, 0.06871825f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log("You're winner!");

            if (!endGame)
            {
                audio.Play();
            }

            endGame = true;
        }
    }
}
