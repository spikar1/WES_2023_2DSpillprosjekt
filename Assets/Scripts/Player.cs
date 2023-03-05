using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float runSpeed = 5;

    [SerializeField]
    float jumpHeight = 12;

    [SerializeField]
    private int health = 10;
    Rigidbody2D rb;
    [SerializeField]
    bool grounded;
    [SerializeField]
    Transform visualTransform;
    private bool justJumped;
    private ContactPoint2D lastContact;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector3.down * 30;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }
    }

    public void OnDamage()
    {
        health -= 1;
    }

    private void FixedUpdate()
    {
        if (!grounded)
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, rb.velocity.y);
        else
            rb.velocity = Input.GetAxis("Horizontal") * -Vector2.Perpendicular(lastContact.normal) * runSpeed;

        if (!grounded)
            visualTransform.up = rb.velocity;
    }

    private void Jump()
    {
        grounded = false;
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * (jumpHeight+0.5f)));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Vector2.Angle(collision.contacts[0].normal, Vector2.up) < 60)
        {
            grounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        visualTransform.up = collision.contacts[0].normal;
        lastContact = collision.contacts[0];
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Vector2.Angle(lastContact.normal, Vector2.up) < 60)
        {
            Debug.DrawRay(lastContact.point, lastContact.normal, Color.magenta, 5);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, .6f);
            if (hit)
            {
                transform.position += Vector3.down * (hit.distance - 0.49f);
                return;
            }
        }
        grounded = false;
    }
}
