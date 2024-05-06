using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    BoxCollider2D enemyCollider;
    Animator animator;
    AIDestinationSetter destinationSetter;
    GameController gameController;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider2D>();
        destinationSetter = GetComponentInParent<AIDestinationSetter>();
        player = GameObject.FindWithTag("Player").transform;
        destinationSetter.target = player;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            bool isEnemyAttacking = collision.gameObject.GetComponent<Human>().isAttacking;
            if (isEnemyAttacking)
            {
                enemyCollider.enabled = false;
                destinationSetter.target = null;
                animator.SetTrigger("Die");
            }
        }
    }

    void Die()
    {
        gameController.score += 1;
        Destroy(this.gameObject);
    }
}
