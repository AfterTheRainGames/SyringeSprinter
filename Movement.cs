using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    public Transform cam;
    private SyringeCheck syringeCheck;
    public Transform killerHand;
    public Transform killerHead;
    private AITracking aiTracking;
    private Transform player;
    public Vector3 spawn = new Vector3(-15.5f, 1, 21);
    public Vector3 winSpot;
    public GameObject doorsTrigger;
    public GameObject doors;
    public bool win;
    private float horizontalRotation;
    private float verticalRotation;

    public float speed = 10;
    public float gravity = -9.8f;
    public float jumpForce = 4;

    private bool isGrounded;

    Vector3 velocity;

    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachinePOV povComponent;
    public AudioSource door;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
        syringeCheck = FindObjectOfType<SyringeCheck>();
        player = GetComponent<Transform>();
        aiTracking = FindObjectOfType<AITracking>();
        povComponent = cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        winSpot = new Vector3 (spawn.x, spawn.y, spawn.z + 10);
        doors.SetActive(true);
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        Vector3 forwardDirection = cam.forward;
        Vector3 rightDirection = cam.right;
        forwardDirection.y = 0;
        rightDirection.y = 0;
        float forwardPos = Input.GetAxisRaw("Vertical"); //W S
        float rightPos = Input.GetAxisRaw("Horizontal"); //A D
        Vector3 moveDirection = (forwardDirection * forwardPos + rightDirection * rightPos);
        speed = syringeCheck.syringeSpeed;

        if (moveDirection.magnitude != 0  && !aiTracking.caught)
        {
            controller.Move(moveDirection * speed * Time.deltaTime);
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (aiTracking.caught)
        {
            cinemachineVirtualCamera.Follow = killerHand;
            povComponent.m_VerticalAxis.Value = killerHead.position.y + 180;
            povComponent.m_HorizontalAxis.Value = (killerHead.position.x + 45  + killerHead.position.z + 180);
        }
        else
        {
            cinemachineVirtualCamera.Follow = player.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Door"))
        {
            if (syringeCheck.syringeCount == 12)
            {
                door.Play();
                win = true;
                syringeCheck.playerLight.range = 0;
                syringeCheck.playerLight.intensity = 0;
                doors.SetActive(false);
            }
        }
    }
}