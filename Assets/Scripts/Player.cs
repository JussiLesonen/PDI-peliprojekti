using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] AudioSource jumpSound;

    public static bool isDead = false;
    private bool isGravityFlipped = false;

    public CharacterController controller;
    public Transform cam;
    public CinemachineFreeLook playerCam;

    public float speed;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    float jumpPECooldown;

    public float turnSmoothTime = 0.1f;

    void Update()
    {
        // Gravity flip
        if (Input.GetKeyDown(KeyCode.H))
        {
            isGravityFlipped = !isGravityFlipped;

            // Change gravity direction
            gravity = isGravityFlipped ? 9.81f : -9.81f;

            velocity.y = 0f;
        }
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!isGravityFlipped)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                jumpSound.Play();
            }
            if (isGravityFlipped)
            {
                velocity.y = -6;
                jumpSound.Play();
            }
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (isDead)
        {
            playerCam.Follow = null;
        }
    }
}
