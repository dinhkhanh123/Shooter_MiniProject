using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    [SerializeField] private float launchAngle;
    [SerializeField] private GameObject pointAppearsPrefab;
  


    public void FireRocket(Vector3 targetPosition)
    {
        if (secondSinceLateShot >= _secondBetweenShots)
        {
            float radianAngle = Mathf.Deg2Rad * launchAngle;
            float distance = Vector3.Distance(transform.position, targetPosition);
            float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radianAngle));

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                GameObject newRocket = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
                Rocket rocket = newRocket.GetComponent<Rocket>();

                if (rocket != null)
                {
                    rocket.Initialize(velocity, launchAngle);
                }

                secondSinceLateShot = 0;
            }

        }
    }

}
