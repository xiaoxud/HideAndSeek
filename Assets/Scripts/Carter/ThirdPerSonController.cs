using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerSonController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpForce = 6f;
    public float gravity = 20f;

    private CharacterController controller;
    private Animator animator;

    private Vector3 moveDirection = Vector3.zero;
    private bool isRunning = false;
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    void HandleMovement()
    {
        if (controller.isGrounded)
        {
            if (isJumping)
            {
                isJumping = false;
                animator.ResetTrigger("Jump");
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 inputDir = new Vector3(h, 0f, v).normalized;

            if (inputDir.magnitude > 0.1f)
            {
                isRunning = Input.GetKey(KeyCode.LeftShift);
                float speed = isRunning ? runSpeed : walkSpeed;

                float angle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

                moveDirection = moveDir.normalized * speed;
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
                animator.SetTrigger("Jump");
                isJumping = true;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void HandleAnimation()
    {
        bool isMoving = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsRunning", isRunning);
    }
}
