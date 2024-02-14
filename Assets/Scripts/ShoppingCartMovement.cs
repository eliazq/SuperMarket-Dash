using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShoppingCartMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    Vector3 velocity = new Vector3(0, 1, 0);
    [Header("References")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Animator animator;
    [Header("Movement Settings")]
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 10f;

    // Ground Check
    bool isGrounded;
    private float groundCheckRaidus = 0.3f;
    bool jump = false; // if true, jump force will be added

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRaidus, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            jump = true;
        if (!isGrounded) jump = false;
    }
    private void FixedUpdate() {
        velocity = new Vector3(velocity.x, rigidbody.velocity.y, velocity.y);
        if (jump) Jump();
        animator.SetFloat("VelocityY", velocity.y);
        rigidbody.velocity = velocity + (Vector3.forward * runSpeed);
    }

    private void Jump()
    {
        velocity.y = jumpForce;
    }
}
