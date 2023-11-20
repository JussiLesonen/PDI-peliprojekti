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
        jumpSound.volume = Options.masterVolume;
        // Gravity flip
        //Change hardcoded gravity change while on roof
        //make sure you check isGrounded from the roof also
        if (Input.GetKeyDown(KeyCode.H) && isGrounded)
        {
            isGravityFlipped = !isGravityFlipped;

            // Change gravity direction
            gravity = isGravityFlipped ? 9.81f : -9.81f;

            //velocity.y = 0f;

            
            //transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, currentRotation.z + 180f);

            //playerCam.transform.rotation *= Quaternion.Euler(0, 0, 180);
            UpdatePlayerCamFollowTarget();


            


        }

        //jump
        Vector3 currentRotation = transform.eulerAngles;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        
        if (!isGravityFlipped)
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }
        else
        {
            if (isGrounded && velocity.y > 0)
            {
                velocity.y = 2f;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!isGravityFlipped && isGrounded)
            {

                //velocity.y = -2f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                jumpSound.Play();
                Debug.Log(velocity.y);
            }
            if (isGravityFlipped && isGrounded)
            {
                //velocity.y = 2f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * -gravity)*-1;
                jumpSound.Play();
                Debug.Log(velocity.y);
            }
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //walk
        if (!isGravityFlipped)
        {
            transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, Mathf.Lerp(currentRotation.z, 0, Time.deltaTime * 10f));
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                // Calculate the movement direction based on the target angle
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // Keep the original Y rotation and only update the position
                Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
                transform.rotation = newRotation;

                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
        else if (isGravityFlipped)
        {
            transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, Mathf.Lerp(currentRotation.z, 180f, Time.deltaTime * 10f));
            float horizontal = -Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                // Calculate the movement direction based on the target angle
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // Keep the original Y rotation and only update the position
                Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
                transform.rotation = newRotation;

                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
        
        void UpdatePlayerCamFollowTarget()
        {
            // Update the playerCam follow target to follow the player's transform
            //playerCam.Follow = transform;
            playerCam.transform.rotation *= Quaternion.Euler(0, 0, 180);
            //playerCam.m_Orbits[0].m_Height = -playerCam.m_Orbits[0].m_Height;
            //playerCam.m_Orbits[1].m_Height = -playerCam.m_Orbits[1].m_Height;
            //playerCam.m_Orbits[2].m_Height = -playerCam.m_Orbits[2].m_Height;
        }


        if (isDead)
        {
            playerCam.Follow = null;
        }
    }
   
}
