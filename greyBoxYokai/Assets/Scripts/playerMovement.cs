using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    private float movementInput;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatisGround;

    private int extraJumps;
    public int extraJumpsVal;


    private void Start() // init stuff here
    {
        extraJumps = extraJumpsVal;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() // used to manage all physics related aspects
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);

        movementInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(movementInput * movementSpeed, rb.velocity.y);

        if (facingRight = false && movementInput > 0)
        {
            flipSprite();
        }else if (facingRight = false && movementInput < 0)
        {
            flipSprite();
        }
    }



    private void Update()  {

        if (isGrounded == true)
        {
            extraJumps = extraJumpsVal;
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }else if(Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void flipSprite()
    {
        facingRight = !facingRight;

        Vector3 Scaler = transform.localScale;

        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}
