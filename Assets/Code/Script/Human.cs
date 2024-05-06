using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Human : MonoBehaviour
{

    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;

    float speed = 1.0f;
    Vector2 movement = new Vector2(0, 0);
    Animator animator;
    public bool isAlive = true;
    public bool isAttacking = false;
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameController.isPaused)
        {
            return;
        }
        if (isAlive)
        {
            rb.velocity = new Vector2(movement[0] * speed, movement[1] * speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }
    void OnMove(InputValue vector)
    {
        if (gameController.isPaused)
        {
            return;
        }
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
        if (gameController.isPaused)
        {
            return;
        }
        animator.SetTrigger("Attack");
        isAttacking = true;
    }

    void FinishAttack()
    {
        isAttacking = false;
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
        if (gameController.isPaused)
        {
            return;
        }
        if (collision.gameObject.tag == ("Enemy"))
        {
            if (!isAttacking)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        animator.SetBool("Dead", true);
        isAlive = false;
        gameController.PlayerDie();
    }
}
