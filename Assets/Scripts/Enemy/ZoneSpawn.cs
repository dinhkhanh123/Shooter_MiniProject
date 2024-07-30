using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoneSpawn : MonoBehaviour
{
    public static Action<int> OnPlayerEnter;

    public BossEnemy bossAppear;
    public Door closeDoor;


    public int indexDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter.Invoke(indexDoor);
            if (indexDoor == 4)
            {
                bossAppear.Appear();
                closeDoor.Close();
            }
            Destroy(gameObject);
        }
    }
}
