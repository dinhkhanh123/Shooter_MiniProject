using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected float damage;
    protected Animator _animator;
     public NavMeshAgent _agent { get; private set; }

    protected HealthSystem _healthSystem;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        _healthSystem = GetComponent<HealthSystem>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>().GetComponent<Animator>();
  
    }

    protected virtual void Update()
    {
        ChasePlayer();
    }

    protected virtual void ChasePlayer()
    {

        if (Reference.thePlayer != null)
        {
           // Vector3 playerPosition = Reference.thePlayer.transform.position;
            //Vector3 vectorToPlayer = playerPosition - transform.position;

            if (IsPlayerInAttackRange())
            {
                AttackPlayer();
            }
            else
            {
                _agent.destination = Reference.thePlayer.transform.position;
                // rb.velocity = vectorToPlayer.normalized * speed;

                // Vector3 playerPositionAtOurHeight = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
                // transform.LookAt(playerPositionAtOurHeight);
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

    protected bool IsPlayerInAttackRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    protected virtual void AttackPlayer()
    {
      
        _animator.SetBool("walking", false);
        _animator.SetBool("attacking",true); 
        _agent.isStopped = true;
    }

    public void AttackTrigger()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                hitCollider.gameObject.GetComponent<HealthSystem>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
     
        GameObject otherGameObject = collision.gameObject;


        if (otherGameObject.CompareTag("Bullet"))
        {
          
            Bullet otherBullet = otherGameObject.GetComponent<Bullet>();

            if (otherBullet != null)
            {
                _healthSystem.TakeDamage(otherBullet.damage);
            }
        }
 
    }

}
