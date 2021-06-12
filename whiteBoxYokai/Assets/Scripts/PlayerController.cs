using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;

    private bool isFacingRight = true;
    private bool isRunning;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Animator anim;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;

    public Transform groundCheck;

    public LayerMask whatIsGround;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update() { // called once per frame
        CheckInput();
        CheckMovementDiretion();
        UpdateAnimations();
    }

    private void FixedUpdate() {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckMovementDiretion() {
        if(isFacingRight && movementInputDirection < 0) {
            Flip();
        }
        else if(!isFacingRight && movementInputDirection > 0){
            Flip();
        }

        if(rb.velocity.x != 0) {
            isRunning = true;
        }
        else {
            isRunning = false;
        }
        
    }

    private void UpdateAnimations() {
        anim.SetBool("isRunning", isRunning);
    }

    private void CheckInput() {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    private void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void ApplyMovement() {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
