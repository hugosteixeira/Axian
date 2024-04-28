using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float minimumSpawnTime;
    [SerializeField]
    private float maximumSpawnTime;

    private float timeUntilSpawn;
    private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void Awake()
    {
        SetTimeUntilSpawn();
    }


    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        bool playerAlive = player.GetComponent<Human>().isAlive;
        if (playerAlive)
        {
            if (timeUntilSpawn <= 0)
            {
                Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}
