using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    float speed = 0.5f;
    Rigidbody2D rigidbody2D;
    bool walking = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool playerAlive = player.GetComponent<Human>().isAlive;

        if (walking && playerAlive)
        {
            Vector2 movement = (player.position - transform.position).normalized;
            rigidbody2D.velocity = new Vector2(movement.x * speed, movement.y * speed);
            GetCardinalDirection(Vector3.Angle(transform.position, player.position));
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    int GetCardinalDirection(float angle)
    {
        // Determine the cardinal direction based on the angle
        if (angle >= -45 && angle < 45)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            return 3;
        }
        else if (angle >= 45 && angle < 135)
        {
            return 2;
        }
        else if (angle >= 135 || angle < -135)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            return 1;
        }
        else
        {
            return 4;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            walking = false;
        }
    }
}
