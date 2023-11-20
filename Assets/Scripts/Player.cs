using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] AudioSource jumpSound;

    public static bool isDead = false;

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

    private void Start()
    {
        floatTimer = floatCap;
    }

    void Update()
    {
        jumpSound.volume = Options.masterVolume;
        floatAudio.volume = floatVolume;

        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumpSound.Play();
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && floatTimer > 0.01f && !isGrounded)
        {
            gravity = -2f;

            floatTimer -= Time.deltaTime;

            if (floatPE.isStopped)
            {
                floatPE.Play();
            }
        }
        else
        {
            gravity = -9.81f;

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

        //fpsText.text = (Mathf.Round(avgFrameRate * 10.0f) * 0.1f).ToString() + " FPS";
        floatValue.text = (Mathf.Round(floatTimer * 10.0f) * 0.1f).ToString();

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
