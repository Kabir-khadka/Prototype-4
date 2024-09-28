using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerupPrefabs;

    private float spawnRange = 9.0f;

    public int enemiesCount;
    public int waveNumber = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //setting up random powerup at the start of the game
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], spawnRandomPosition(), powerupPrefabs[randomPowerup].transform.rotation);


        spawnEnemyWave(waveNumber);


    }

    // Update is called once per frame
    
    void Update()
    {
        enemiesCount = FindObjectsOfType<Enemy>().Length;
        int powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;//checks if any poweup exists

        //logic to check if there is any enemy and if there is any powerup in the scene
        if (enemiesCount == 0)
        {
            waveNumber++;
            spawnEnemyWave(waveNumber);

            
        }

        if (enemiesCount == 0 && powerupCount == 0)
        {
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], spawnRandomPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        }
        
    }

    //creating seperate method for instanciation with parameter int enemiesToSpwan
    private void spawnEnemyWave(int enemiesToSpwan)
    {

        for (int i = 0; i < enemiesToSpwan; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefab.Length);

            Instantiate(enemyPrefab[randomEnemy], spawnRandomPosition(), enemyPrefab[randomEnemy].transform.rotation);
        }

    }

    private Vector3 spawnRandomPosition()
    {

        //Storing the x-axis and z-axis random position code inside seperate variables to clean up the code.
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
