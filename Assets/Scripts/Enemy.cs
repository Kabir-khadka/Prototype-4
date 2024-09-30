using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 1.0f;

    private Rigidbody enemyRb;
    private GameObject player;

    //For boss enemy
    public bool isBoss = false;

    public float spawnInterval;
    private float nextSpawn;

    public int miniEnemySpawnCount;

    private SpawnManager spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Getting required components and properties
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if (isBoss)
        {

            spawnManager = FindObjectOfType<SpawnManager>();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //creating a variable to make below code shorter
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        //Adding force to the enemy by subtracting the players position with the enemy's position
        enemyRb.AddForce(lookDirection * speed);

        if (isBoss)
        {

            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;

                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

        
    }
}
