using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform player;
     private static int bulletHits = 0;
    public int maxBulletHits = 5;


    void OnTriggerEnter(Collider other)
    {

        // Chekck if bullet hits the player
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
           bulletHits++;
           Debug.Log("Bullet hits player");

            // Check if there's enough hits to kill the player
            if (bulletHits >= maxBulletHits)
                {
                    Player.isDead = true;
                    bulletHits = 0;
                    Debug.Log("Player died");
                }

            Destroy(gameObject);

        }
    }
}
