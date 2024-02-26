using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShoppingCartMovement : MonoBehaviour
{
    private Rigidbody rb;
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
    private float groundCheckRaidus = 0.4f;
    bool jump = false; // if true, jump force will be added

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRaidus, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            jump = true;
        if (!isGrounded)
        {
            jump = false;
            SoundManager.StopSoundAudioSource(SoundManager.Sound.PlayerMove);
        }
        else SoundManager.PlaySoundAudioSource(SoundManager.Sound.PlayerMove);
    }
    private void FixedUpdate() {
        velocity = new Vector3(velocity.x, rb.velocity.y, runSpeed);
        if (jump) Jump();
        animator.SetFloat("VelocityY", velocity.y);
        rb.velocity = velocity;
    }

    public void ResetAnim()
    {
        animator.SetFloat("VelocityY", 0f);
    }

    private void Jump()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerJump);
        velocity.y = jumpForce;
    }
}
