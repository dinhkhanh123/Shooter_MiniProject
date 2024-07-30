using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> enemySpawners;
    [SerializeField] private List<WeaponSpawn> spawnWeapon;
    [SerializeField] private List<GameObject> zoneSpawn;

    public int currentEnemyAlive { get; private set; }
    
    public int roomCurrent { get; private set; }


    private void Awake()
    {
        roomCurrent = -1;
        Reference.levelManager = this;
        ZoneSpawn.OnPlayerEnter += PlayerEnterRoom;
        HealthSystem.OnEnemyDeath += EnemyDied;
    }


    private void OnDestroy()
    {
        ZoneSpawn.OnPlayerEnter -= PlayerEnterRoom;
        HealthSystem.OnEnemyDeath -= EnemyDied;
    }

    public void OpenDoor() => roomCurrent++;
    public void EnemyDied() => currentEnemyAlive--;

    private void PlayerEnterRoom(int roomId)
    {
        enemySpawners[roomId].gameObject.SetActive(true);
        spawnWeapon[roomId].gameObject.SetActive(true);
        currentEnemyAlive = enemySpawners[roomId].enemiesToSpawn;
    }

}
