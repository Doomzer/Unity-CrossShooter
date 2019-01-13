using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //public List<string> gameType = new List<string>() {"WS_control","Side Scroller"};

    public GameObject enemyToSpawn;
    public bool canSpawn = true;
    public float enemySpawnTime = 1.0f;
    public float minEnemySpawnTime = 1.0f;

    public float maxX = 40.0f;
    public float maxY = 40.0f;

    public float startingX = 25.0f;
    public float startingY = 25.0f;

    float randomX = 0.0f;
    float randomY = 0.0f;

    public int spawnType = 2;

    private float enemySpawnTimer;

    public Player_Control player;

    // Use this for initialization
    void Start ()
    {
        if (spawnType == 1)
            StartCoroutine(SpawnEnemyTimer());

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (spawnType == 2)
            SpawnEnemy();
    }

    IEnumerator SpawnEnemyTimer()
    {
        while (canSpawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(enemySpawnTime);
        }
    }

    void SpawnEnemy()
    {
        if (spawnType == 1)
        {
            randomX = Random.Range(-maxX, maxX);
            randomY = Random.Range(-maxY, maxY);

            Instantiate(enemyToSpawn, new Vector3(randomX, randomY, 0), Quaternion.identity);
        }
        else if (spawnType == 2)
        {
            enemySpawnTimer -= Time.deltaTime;
            if (enemySpawnTimer <= 0 && player != null)
            {

                enemySpawnTime *= 0.9f;
                if (enemySpawnTime < minEnemySpawnTime)
                    enemySpawnTime = minEnemySpawnTime;

                enemySpawnTimer = enemySpawnTime;
                Vector3[] spawnPositions = new Vector3[] { new Vector3(maxX, maxY, 0), new Vector3(-maxX, maxY, 0), new Vector3(maxX, -maxY, 0), new Vector3(-maxX, -maxY, 0) };
                foreach(Vector3 position in spawnPositions)
                {
                    GameObject enemyObject = Instantiate(enemyToSpawn);
                    enemyObject.transform.position = position;
                    enemyObject.transform.SetParent(this.transform);

                    EnemyLogic enemy = enemyObject.GetComponent<EnemyLogic>();

                    enemy.movementDirection = (player.transform.position - position).normalized;
                }
            }
        }
    }
}
