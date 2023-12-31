using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public AudioSource tickSound;

    void Start()
    {
        coins = 0;
        hasKey= false;
        coinsText = GameObject.Find("Coins Text").GetComponent<Text>();
    }

    public static bool hasKey=false;

    [SerializeField] AudioSource coinSound;

    public static int coins = 0;

    [SerializeField] public static Text coinsText;
    
    private void Update()
    {
        coinSound.volume = Options.masterVolume;
        tickSound.volume = Options.masterVolume;

        if (coins > Customization.totalCoins)
        {
            Customization.totalCoins = coins;
        }

        if (hasKey)
        {
            if (!tickSound.isPlaying)
            {
                tickSound.Play();
            }
        }
        else
        {
            if (tickSound.isPlaying)
            {
                tickSound.Stop();
            }
        }
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
            coinsText.text = coins.ToString();

            coinSound.pitch = Random.RandomRange(0.8f, 1.2f);
            coinSound.Play();
        }
        if (other.gameObject.tag=="Key")
        {
            Instantiate(Resources.Load("KeyCollect"), transform);
            coinSound.pitch = Random.RandomRange(0.8f, 1.2f);
            coinSound.Play();
            Destroy(other.gameObject);
            hasKey = true;
        }
            
        if (other.gameObject.CompareTag("Door")&& hasKey)
        {
            Debug.Log("ez");
        }
    }
}
