using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] AudioSource jumpSound;

    public static bool isDead = false;
    private bool isGravityFlipped = false;

    public CharacterController controller;
    public Transform cam;
    public CinemachineFreeLook playerCam;
    public TextMeshProUGUI floatValue;
    public ParticleSystem floatPE;
    public AudioSource floatAudio;

    public float speed;
    public float gravity;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    float floatTimer;
    float floatCap = 1.5f;
    float floatVolume;

    public float turnSmoothTime = 0.1f;

    public static bool canUseHover = false;
    public static bool canUseAntiGrav = false;

    private void Start()
    {
        floatTimer = floatCap;
    }

    void Update()
    {
        jumpSound.volume = Options.masterVolume;
        floatAudio.volume = floatVolume;
        // Gravity flip
        //Change hardcoded gravity change while on roof
        //make sure you check isGrounded from the roof also
        if (Input.GetKeyDown(KeyCode.Tab) && isGrounded && canUseAntiGrav)
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
                //Debug.Log(velocity.y);
               
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

        if (Input.GetKey(KeyCode.LeftShift) && floatTimer > 0.01f && !isGrounded && canUseHover)
        {
            if (!isGravityFlipped)
            {
                gravity = -2f;
            }
            else
            {
                gravity = 2f;
            }

            floatTimer -= Time.deltaTime;

            if (floatPE.isStopped)
            {
                floatPE.Play();
            }
        }
        else
        {
            if (!isGravityFlipped)
            {
                gravity = -9.81f;
            }
            else
            {
                gravity = 9.81f;
            }

            floatTimer += Time.deltaTime * 0.5f;

            if (floatPE.isPlaying)
            {
                floatPE.Stop();
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && floatTimer > 0.1f && !isGrounded)
        {
            floatVolume = Mathf.SmoothStep(floatVolume, Options.masterVolume, Time.deltaTime * 10f);
        }
        else
        {
            floatVolume = Mathf.SmoothStep(floatVolume, 0, Time.deltaTime * 20f);
        }

        if (floatTimer < 0)
        {
            floatTimer = 0;
        }
        else if (floatTimer > floatCap)
        {
            floatTimer = floatCap;
        }

        floatValue.text = (Mathf.Round(floatTimer * 10.0f) * 0.1f).ToString();

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

        if (isDead)
        {
            playerCam.Follow = null;
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
}
