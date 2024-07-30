using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOpenDoor : MonoBehaviour
{
    private Door door;

    private void Awake()
    {
        door = GetComponentInParent<Door>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.Open();
        }
    }
}
