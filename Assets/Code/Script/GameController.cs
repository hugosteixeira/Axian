using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Transform player;
    private GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        gameOverUI = GameObject.FindWithTag("GameOverUI");
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool playerAlive = player.GetComponent<Human>().isAlive;
        if (!playerAlive)
        {
            gameOverUI.SetActive(true);
        }
    }
}
