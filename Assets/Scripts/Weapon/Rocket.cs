using UnityEngine;

public class Rocket : Bullet
{
    public float launchAngle;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private GameObject explosionPrefab;

    public void Initialize(float speed, float angle)
    {
        forceSpeed = speed;
        launchAngle = angle;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;

        Vector3 velocity = CalculateLaunchVelocity();
        _rb.velocity = velocity;
    }

    void FixedUpdate()
    {
        Vector3 gravityForce = new Vector3(0, -gravity, 0);
        _rb.AddForce(gravityForce, ForceMode.Acceleration);
    }

    private Vector3 CalculateLaunchVelocity()
    {
        float radianAngle = Mathf.Deg2Rad * launchAngle;
        float cos = Mathf.Cos(radianAngle);
        float sin = Mathf.Sin(radianAngle);
        Vector3 velocity = transform.forward * forceSpeed * cos + Vector3.up * forceSpeed * sin;
        return velocity;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        Instantiate(explosionPrefab, transform.position, transform.rotation);
       
    }
}
