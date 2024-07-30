using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private GameObject telePort;
    [SerializeField] private float secondsBetweenSpawns;
    [SerializeField] private int numberOfSpawnPoints;
    [SerializeField] private Vector2 xRange = new Vector2(-25, 25);
    [SerializeField] private Vector2 zRange = new Vector2(0, 50);
    [field:SerializeField]  public int enemiesToSpawn { get; private set; }

    private List<Vector3> spawnPoints = new List<Vector3>();
    private List<GameObject> spawnPointObjects = new List<GameObject>();
    private float secondsSinceLastSpawn;

    private void Awake()
    {
        HealthSystem.OnEnemyDeath += DeleteSpawnPoint;
    }

    private void OnDestroy()
    {
        HealthSystem.OnEnemyDeath -= DeleteSpawnPoint;
    }
    void Start()
    {
        secondsSinceLastSpawn = 0f;
        GenerateSpawnPoints();
    }

    void Update()
    {
        secondsSinceLastSpawn += Time.deltaTime;

        if (secondsSinceLastSpawn >= secondsBetweenSpawns && enemiesToSpawn > 0)
        {
           
            SpawnEnemy();
            secondsSinceLastSpawn = 0f;
        }


    }

    void GenerateSpawnPoints()
    {
        for (int i = 0; i < numberOfSpawnPoints; i++)
        {
            float randomX = Random.Range(xRange.x, xRange.y);
            float randomZ = Random.Range(zRange.x, zRange.y);
            Vector3 spawnPoint = new Vector3(randomX, 2, randomZ);
            spawnPoints.Add(spawnPoint);
            GameObject spawnPointObject =   Instantiate(telePort, spawnPoint, Quaternion.identity);
            spawnPointObjects.Add(spawnPointObject);
        }
    }

    void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        Vector3 spawnPointPosition = spawnPoints[spawnPointIndex];

        int enemyIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemyPrefab = enemyPrefabs[enemyIndex];

       Instantiate(enemyPrefab, spawnPointPosition, Quaternion.identity);

        enemiesToSpawn--;
        
    }

    public void DeleteSpawnPoint()
    {
        if(Reference.levelManager.currentEnemyAlive == 0)
        {
            foreach (GameObject spawn in spawnPointObjects)
            {
                Destroy(spawn);
            }
            spawnPointObjects.Clear();
        }
    }
 


}
