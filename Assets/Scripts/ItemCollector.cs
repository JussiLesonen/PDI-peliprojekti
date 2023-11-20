using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    void Start()
    {
        coins = 0;
        hasKey= false;
        Debug.Log(hasKey);
    }

    bool hasKey=false;

    [SerializeField] AudioSource coinSound;

    public static int coins = 0;

    [SerializeField] Text coinsText;

    private void Update()
    {
        coinSound.volume = Options.masterVolume;

        //Debug.Log(Options.masterVolume);
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
            Debug.Log(hasKey);
        }
        if (other.gameObject.tag=="Key")
        {
            Destroy(other.gameObject);
            hasKey = true;
            Debug.Log(other.gameObject.tag);
            Debug.Log(hasKey);
        }
            
        if (other.gameObject.CompareTag("Door")&& hasKey)
        {
            Debug.Log("ez");
        }
    }
}
