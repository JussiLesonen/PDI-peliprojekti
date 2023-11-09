using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3.0f;
    public float detectionRange = 5.0f;

    private bool isPatrolling = false;
    private Vector3 originalPosition;
    public float patrolRange = 3.0f;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        originalPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Player is detected, switch to follow player state
            isPatrolling = false;
            FollowPlayer();
        }
        else
        {
            // Player is not detected, switch to patrol state
            Patrol();
        }
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
            // Move towards the original position
            Vector3 direction = originalPosition - transform.position;
            direction.Normalize();
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Check if reached the original position
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

            // Check if reached the patrol range limit
            float distanceToPatrolLimit = Mathf.Abs(transform.position.x - originalPosition.x);
            if (distanceToPatrolLimit >= patrolRange)
            {
                isPatrolling = false;
            }
        }
    }
}
