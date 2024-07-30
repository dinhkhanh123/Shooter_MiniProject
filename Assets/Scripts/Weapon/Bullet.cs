using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float forceSpeed;
    public float secondsUntilDestroy;
    protected Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * forceSpeed;
    }

    void Update()
    {
        secondsUntilDestroy -= Time.deltaTime;

        if (secondsUntilDestroy < 0)
        {
            Destroy(gameObject);
        }
    }


    protected virtual void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
