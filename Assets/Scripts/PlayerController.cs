using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //     private CharacterController controller;
    //     private Vector3 playerVelocity;
    //     private bool groundedPlayer;
    //     private float playerSpeed = 2.0f;
    //     private float jumpHeight = 1.0f;
    //     private float gravityValue = -1f;

    //     private void Start()
    //     {
    //         controller = gameObject.AddComponent<CharacterController>();
    //     }

    //     void Update()
    //     {
    //         // groundedPlayer = controller.isGrounded;
    //         // if (groundedPlayer && playerVelocity.y < 0)
    //         // {
    //         //     playerVelocity.y = 0f;
    //         // }

    //         // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //         // controller.Move(move * Time.deltaTime * playerSpeed);

    //         // if (move != Vector3.zero)
    //         // {
    //         //     gameObject.transform.forward = move;
    //         // }

    //         // Changes the height position of the player..
    //         if (Input.GetButtonDown("Jump"))
    //         {
    //             playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    //         }

    //         playerVelocity.y += gravityValue * Time.deltaTime;
    //         controller.Move(playerVelocity * Time.deltaTime);
    //     }
    // }

    // CharacterController characterController;
    // public float MovementSpeed = 1f;
    // public float Gravity = 9.8f;
    // private float velocity = 0;

    // private void Start()
    // {
    //     characterController = GetComponent<CharacterController>();
    // }

    // void Update()
    // {
    //     // player movement - forward, backward, left, right
    //     float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
    //     float vertical = Input.GetAxis("Vertical") * MovementSpeed;

    //     Camera camera = Camera.main;
    //     Vector3 forward = camera.transform.forward;
    //     Vector3 right = camera.transform.right;

    //     forward.y = 0f;
    //     right.y = 0f;
    //     forward.Normalize();
    //     right.Normalize();

    //     Vector3 desiredMoveDirection = forward * vertical + right * horizontal;

    //     characterController.Move(desiredMoveDirection * MovementSpeed * Time.deltaTime);


    //     // characterController.Move((Vector3.right * horizontal + Vector3.forward * vertical) * Time.deltaTime);

    //     // // Gravity
    //     // if (characterController.isGrounded)
    //     // {
    //     //     velocity = 0;
    //     // }
    //     // else
    //     // {
    //     //     velocity -= Gravity * Time.deltaTime;
    //     //     characterController.Move(new Vector3(0, velocity, 0));
    //     // }
    // }

    private float speed = 100f;
    private float walkSpeed = 0.5f;
    private float runSpeed = 10f;

    private float gravity = 8;

    private Rigidbody body;
    // private Animator anim;

    private Vector3 direction;
    float percent;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        // anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {



        Move();

        if (direction != Vector3.zero)
        {
            HandleRotation();
        }

        // HandleAnimations();
    }

    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direction = new Vector3(h, 0, v);

        direction = direction.normalized;

        if (Input.GetButton("Fire1"))
        {
            percent = runSpeed * direction.magnitude;
            speed = 200f;
        }
        else
        {
            percent = walkSpeed * direction.magnitude;
            speed = 100f;
        }

        //CONVERT direction from local to world relative to camera
        body.velocity = Camera.main.transform.TransformDirection(direction) * speed * Time.deltaTime;
    }

    public void HandleRotation()
    {
        float targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        Quaternion lookAt = Quaternion.Slerp(transform.rotation,
                            Quaternion.Euler(0, targetRotation, 0),
                            0.5f);
        body.rotation = lookAt;

    }


    // public void HandleAnimations()
    // {
    //     anim.SetFloat("speed", percent, 0.1f, Time.deltaTime);
    // }
}