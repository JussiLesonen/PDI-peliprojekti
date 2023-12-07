using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] AudioSource jumpSound;
    [SerializeField] Text healthText;

    public static int health = 5;
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
    public static float floatTimer;
    float floatCap = 1.5f;
    float floatVolume;
    public static float damageCooldown;

    public GameObject bluearrow;
    public GameObject redarrow;

    public Slider angleSlider;

    public void Teleport(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        Physics.SyncTransforms();
        Debug.Log("teepee");
    }

    public float turnSmoothTime = 0.1f;

    public static bool canUseHover = false;
    public static bool canUseAntiGrav = false;

    private void Start()
    {
        floatTimer = floatCap;

        canUseHover = false;
        canUseAntiGrav = false;
    }

    void Update()
    {
        if (damageCooldown < 0)
        {
            damageCooldown = 0;
        }

        damageCooldown -= Time.deltaTime;

        healthText.text = "Health: " + health;

        jumpSound.volume = Options.masterVolume;
        floatAudio.volume = floatVolume;
        // Gravity flip
        //Change hardcoded gravity change while on roof
        //make sure you check isGrounded from the roof also
        if (Input.GetKeyDown(KeyCode.Tab) && isGrounded && canUseAntiGrav)
        {
            isGravityFlipped = !isGravityFlipped;
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
            bluearrow.SetActive(true);
            redarrow.SetActive(false);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }
        else
        {
            bluearrow.SetActive(false);
            redarrow.SetActive(true);
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
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * -gravity) * -1;
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

        if (Input.GetKey(KeyCode.LeftShift) && floatTimer > 0.1f && !isGrounded && canUseHover)
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
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
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
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
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
    public static void AddBulletHits(int amount)
    {
        health = Mathf.Max(0, health - amount);

        if (health <= 0)
        {
            isDead = true;
            Debug.Log("Player died");
        }
    }
}
