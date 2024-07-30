using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyController
{

    [SerializeField] private Weapon myWeapon;
    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        FindPlayer();
    }

    public void FindPlayer()
    {

        if (Reference.thePlayer != null)
        {
            if (IsPlayerInAttackRange())
            { 
                _agent.isStopped = true;
                _animator.SetBool("attacking", true);
                myWeapon.Fire(PlayerPosition());
                transform.LookAt(PlayerPosition());
            }
            else
            {
                _agent.destination = Reference.thePlayer.transform.position;
                _animator.SetBool("walking", true);
                _animator.SetBool("attacking", false);
                _agent.isStopped = false;
            }
        }
        else
        {
            _animator.SetBool("walking", false);
        }
    }

    public Vector3 PlayerPosition()
    {
        return Reference.thePlayer.transform.position;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

}