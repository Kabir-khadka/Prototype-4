using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRange = 9.0f;

    public int enemiesCount;
    public int waveNumber = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            Instantiate(powerupPrefab, spawnRandomPosition(), powerupPrefab.transform.rotation);
        }
        
    }

    //creating seperate method for instanciation with parameter int enemiesToSpwan
    private void spawnEnemyWave(int enemiesToSpwan)
    {

        for (int i = 0; i < enemiesToSpwan; i++)
        {
            Instantiate(enemyPrefab, spawnRandomPosition(), enemyPrefab.transform.rotation);
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
