using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    [SerializeField] AudioSource coinSound;

    public static int coins = 0;

    [SerializeField] Text coinsText;

    private void Update()
    {
        coinSound.volume = Options.masterVolume;

        Debug.Log(Options.masterVolume);
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            // Particle effect
            Instantiate(Resources.Load("CoinCollect"), transform);

            Destroy(other.gameObject);
            coins++;
            //Debug.Log("Coins: " + coins);
            coinsText.text = "Coins: " + coins;

            coinSound.pitch = Random.RandomRange(0.8f, 1.2f);
            coinSound.Play();
        }
    }
}
