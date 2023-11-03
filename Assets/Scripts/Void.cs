using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    float timer;

    private void Start()
    {
        timer = 3f;
    }

    private void Update()
    {
        if (Player.isDead)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Player.isDead = true;

            Debug.Log(Player.isDead);
        }
    }
}
