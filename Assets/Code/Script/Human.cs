using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Human : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    BoxCollider2D boxCollider2D;

    float speed = 2.0f;
    Vector2 movement = new Vector2(0, 0);
    Animator animator;
    public bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            rigidbody2D.velocity = new Vector2(movement[0] * speed, movement[1] * speed);
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }

    }
    void OnMove(InputValue vector)
    {
        Vector2 vec = vector.Get<Vector2>();
        movement = vec.normalized;
        if (isAlive)
        {
            if (movement == Vector2.zero)
            {
                animator.SetBool("Walking", false);
            }
            else
            {
                UpdateDirection(movement);
                animator.SetBool("Walking", true);
            }
        }
    }

    void OnAttack()
    {
        animator.SetTrigger("Attack");
    }

    void UpdateDirection(Vector2 vector)
    {
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        // Determine the cardinal direction based on the angle
        animator.SetInteger("Direction", GetCardinalDirection(angle));

    }


    int GetCardinalDirection(float angle)
    {
        // Determine the cardinal direction based on the angle
        if (angle >= -45 && angle < 45)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            boxCollider2D.transform.localRotation = Quaternion.Euler(0, 0, 0);
            return 3;
        }
        else if (angle >= 45 && angle < 135)
        {
            return 2;
        }
        else if (angle >= 135 || angle < -135)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            boxCollider2D.transform.localRotation = Quaternion.Euler(0, 180, 0);
            return 1;
        }
        else
        {
            return 4;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            AnimatorClipInfo[] nextAnimation = animator.GetNextAnimatorClipInfo(0);
            if (nextAnimation.Length != 0 && nextAnimation[0].clip.name.Contains("attack"))
            {
                Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
                enemyAnimator.SetTrigger("Die");
                Destroy(collision.gameObject);
            }
            else
            {
                animator.SetBool("Dead", true);
                isAlive = false;
            }
        }
    }
}
