using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Knight : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;
    float jumpForce = 3;
    float speed = 2.0f;
    bool attacking = false;
    float horizontalInput;

    bool isGrounded;
    [SerializeField] private LayerMask platformLayer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontalInput * speed, rigidbody2D.velocity.y);
    }

    public void OnHeavyAttack()
    {
        attacking = true;
        animator.StopPlayback();
        animator.SetTrigger("Attack");
        attacking = false;
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            animator.StopPlayback();
            animator.SetTrigger("Jump");
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnWalk(InputValue axis)
    {
        horizontalInput = axis.Get<float>();
        if (isGrounded && !attacking)
        {
            animator.SetBool("Walking", true);
        }
        if (horizontalInput < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horizontalInput > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = false;
        }
    }
}
