using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [FormerlySerializedAs("health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private GameObject deathEffectPrefab;
    

    HealthBar myHealthBar;
    private EnemyController myEnemyController;
    private float currentHealth;
    private Vector3 healthBarOffset = Vector3.up * 100f; 
    private float smoothSpeed = 100f;

    public static Action OnEnemyDeath;
    

    void Start()
    {
        currentHealth = maxHealth;
        myEnemyController = GetComponent<EnemyController>();    
        GameObject healthBarObject = Instantiate(healthBarPrefab, Reference.canvas.transform);
        myHealthBar = healthBarObject.GetComponent<HealthBar>();
    }


    void Update()
    {
        myHealthBar.ShowHealthFraction(currentHealth/maxHealth);          
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Camera.main.WorldToScreenPoint(transform.position) + healthBarOffset;
        myHealthBar.transform.position = Vector3.Lerp(myHealthBar.transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
    public void TakeDamage(float damageAmount)
    {

        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                if (myEnemyController != null)
                {
                    if (myEnemyController is BossEnemy)
                    {
                        ((BossEnemy)myEnemyController).HandleDeath();
                    }
                    else
                    {
                        OnEnemyDeath?.Invoke();
                    }

                }
          
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (myHealthBar != null)
        {
            Destroy(myHealthBar.gameObject);
        }
    }
}
