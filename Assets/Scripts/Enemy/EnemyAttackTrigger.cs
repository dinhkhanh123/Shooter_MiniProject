using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    private EnemyController _enemyController;


    private void Start()
    {
        _enemyController = GetComponentInParent<EnemyController>();
    }

    private void AttackTrigger() => _enemyController.AttackTrigger();
}
