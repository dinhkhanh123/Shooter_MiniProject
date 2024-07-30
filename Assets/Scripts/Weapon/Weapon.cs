using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Weapon : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private float accuracy;
    [SerializeField] public float _secondBetweenShots;
    [SerializeField] public float numberOfProjectiles;

    [SerializeField] private AudioSource audioSource;
    protected float secondSinceLateShot;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        secondSinceLateShot = _secondBetweenShots;
        
    }

    void Update()
    {
        secondSinceLateShot += Time.deltaTime;
    }



    public void Fire(Vector3 targetPostion)
    {

        if (secondSinceLateShot >= _secondBetweenShots)
        {
             audioSource.Play();

            for (
                int iterationCount = 0; 
                iterationCount < numberOfProjectiles; 
                iterationCount++ 
            )
            {

                GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
                Bullet bulletScript = newBullet.GetComponent<Bullet>();
                


                float inaccuracy = Vector3.Distance(transform.position, targetPostion) / accuracy;
                Vector3 inaccuratePosition = targetPostion;
                inaccuratePosition.x += Random.Range(-inaccuracy, inaccuracy);
                inaccuratePosition.z += Random.Range(-inaccuracy, inaccuracy);

                newBullet.transform.LookAt(inaccuratePosition);
                secondSinceLateShot = 0;
            }
        }
    }

    


}
