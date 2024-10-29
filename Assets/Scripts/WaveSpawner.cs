using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
	public int maxWave;
	public int currentWave;
	public int waveValMulti = 4;
	public int waveValue;
    public int currentWaveValue;
	public List<GameObject> enemiesToSpawn = new List<GameObject>();
	public int spawnedEnemiesCount = 0;

	public List<Transform> spawnLocations = new List<Transform>();
    public int waveDuration = 10;
    public float spawnInterval = 0.5f;

	[SerializeField] private float waveTimer;
	[SerializeField] private float spawnTimer;

    void Start()
    {
        GenerateWave();
    }

    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                EnemySpawn();
                spawnTimer = spawnInterval;
            }
			else
			{
				waveTimer -= Time.fixedDeltaTime;
			}
		}
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

		if (waveTimer <= 0)
		{
			currentWave++;
            if (currentWave <= maxWave)
            {
				GenerateWave();
			}
		}
	}

    public int SpawnPointRandom()
    {
        int randSpawnId = 0;

		if (currentWave < 5)
		{
			randSpawnId = Random.Range(0, 9);
		}
		else if (currentWave > 5)
		{
			randSpawnId = Random.Range(0, spawnLocations.Count);
		}

        return randSpawnId;
	}

    public void EnemySpawn()
    {
        int randSpawnId = SpawnPointRandom();
        if (randSpawnId < 17)
        {
			GameObject enemy = Instantiate(enemiesToSpawn[0], spawnLocations[randSpawnId].position, Quaternion.identity);
		}
        else if (randSpawnId < 26)
        {
			GameObject enemy = Instantiate(enemiesToSpawn[0], spawnLocations[randSpawnId].position, Quaternion.Euler(0, 0, 45));
		}
        else if (randSpawnId < 35)
		{
			GameObject enemy = Instantiate(enemiesToSpawn[0], spawnLocations[randSpawnId].position, Quaternion.Euler(0, 0, -45));
		}
		enemiesToSpawn.RemoveAt(0);
        spawnedEnemiesCount++;
	}

    public void SomeoneIsKilled()
    {
        spawnedEnemiesCount--;

		if (spawnedEnemiesCount <= 0)
		{
			waveTimer = 2f;
		}
	}

    public void GenerateWave()
    {
		currentWaveValue = currentWave * waveValMulti;
        waveValue = currentWaveValue;
        GenerateEnemy();
        waveTimer = waveDuration;
    }

    public void GenerateEnemy()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (currentWaveValue > 0 || generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (currentWaveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
				currentWaveValue -= randEnemyCost;
            }
            else if (currentWaveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}