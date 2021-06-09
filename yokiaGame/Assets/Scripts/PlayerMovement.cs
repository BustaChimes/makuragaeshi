using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public Rigidbody2D rb;

    public float jumpForce;
    public Transform feet;
    public LayerMask groundLayers;

    float xAxisSpeed;
    int nHops = 0;

    private void Update()
    {
        xAxisSpeed = Input.GetAxisRaw("Horizontal") * playerSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            if (nHops < 2)
            {
                Jump();
                nHops++;
            }
            else { OnGround(); }
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(xAxisSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    void Jump()
    {
        Vector2 jump = new Vector2(rb.velocity.x, jumpForce);
        rb.velocity = jump;
    }

    public bool OnGround()
    {
        Collider2D check = Physics2D.OverlapCircle(feet.position, 0.05f, groundLayers);
        if (check != null)
        {
            nHops = 0;
            return true;
        }
        return false;
    }
}
