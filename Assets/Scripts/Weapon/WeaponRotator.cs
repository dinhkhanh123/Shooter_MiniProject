using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    public float rotationSpeed = 360f;
    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime );
    }
}
