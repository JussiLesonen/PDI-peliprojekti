using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3.0f;
    public float detectionRange = 5.0f;
    public GameObject bullet;
    public Transform spawnPoint;
    public float shootCooldown = 2.0f;
    public float bulletSpeed = 10.0f;
    public LayerMask playerLayer;
    [SerializeField]private float timer = 5;
    private float bulletTime;
    private float lastShootTime;
    private bool isPatrolling = false;
    private Vector3 originalPosition;
    public float patrolRange = 3.0f;
    [SerializeField] AudioSource ShootingSound;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        originalPosition = transform.position;
        lastShootTime = Time.time;
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Player is detected, switch to follow player state
            isPatrolling = false;
            FollowPlayer();

            // Shoot at the player
            if (Time.time - lastShootTime > shootCooldown)
            {
                Shoot();
                lastShootTime = Time.time;
                ShootingSound.Play();
            }
        }
        else
        {
            // Player is not detected, switch back to patrol state
            Patrol();
        }

        Shoot();
    }

    void FollowPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void Patrol()
    {
        if (!isPatrolling)
        {
            Vector3 direction = originalPosition - transform.position;
            direction.Normalize();
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            float distanceToOriginal = Vector3.Distance(transform.position, originalPosition);
            if (distanceToOriginal < 0.1f)
            {
                isPatrolling = true;
            }
        }
        else
        {
            // Move within the patrol range
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            float distanceToPatrolLimit = Mathf.Abs(transform.position.x - originalPosition.x);
            if (distanceToPatrolLimit >= patrolRange)
            {
                isPatrolling = false;
            }
        }
    }

    void Shoot()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime <= 0)
        {
            bulletTime = timer;

            // Check if the player is close before shooting
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRange)
            {
                GameObject bulletObj = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation) as GameObject;
                Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
                Vector3 shootDirection = (player.position - spawnPoint.position).normalized;
                bulletRig.AddForce(shootDirection * bulletSpeed);

                Destroy(bulletObj, 1.0f);
            }
        }
    }
}
