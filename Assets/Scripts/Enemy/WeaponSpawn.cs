using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponPrefabs;
   
    [SerializeField] private Vector2 xRange = new Vector2(-25, 25);
    [SerializeField] private Vector2 zRange = new Vector2(0, 50);

    [SerializeField] private float rotationSpeed = 360f;
    private void Start()
    {
        SpawnWeapon();
    }

    void SpawnWeapon()
    {
       for(int i = 0; i < weaponPrefabs.Count; i++)
        {
            GameObject weapon =  Instantiate(weaponPrefabs[i], GenerateSpawnPoints(), Quaternion.identity);
            WeaponRotator rotator = weapon.AddComponent<WeaponRotator>();
            rotator.rotationSpeed = rotationSpeed;
        }
    }

    Vector3 GenerateSpawnPoints()
    {
   
            float randomX = Random.Range(xRange.x, xRange.y);
            float randomZ = Random.Range(zRange.x, zRange.y);
            Vector3 spawnPoint = new Vector3(randomX, 1, randomZ);

        return spawnPoint;
    }

}
