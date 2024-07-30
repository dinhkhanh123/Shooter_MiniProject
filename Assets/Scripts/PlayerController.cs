using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _anim;
    [SerializeField] private Transform weaponPos;
    [SerializeField] private float speed;
    [SerializeField] private float runSpeedMultiplier;
    public List<Weapon> weapons = new List<Weapon>();
    [SerializeField] private int selectedWeaponIndex;
    private Vector3 cursorPosition;
    private bool canMove = true;
    private bool wasMoving = false;
    private Vector3 targetPosition;
    private bool isAiming = false;
    private bool isRocketLauncherEquipped = false;

    private HealthSystem _healthSystem;

    private void Awake()
    {
        Reference.thePlayer = this;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _healthSystem = GetComponent<HealthSystem>();
        _anim = GetComponentInChildren<Animator>().GetComponent<Animator>();
        selectedWeaponIndex = 0;
        CheckIfRocketLauncherEquipped();
    }

    void Update()
    {
        RotateTowardsMouse();
        InputMouse();
        Movement();
    }

    private void InputMouse()
    {
        if (weapons.Count > 0 && Input.GetMouseButton(0))
        {
            if (isRocketLauncherEquipped && !isAiming)
            {
                
                isAiming = true;
            }
            else if (!isRocketLauncherEquipped)
            {
                
                StartShooting();
            }
        }
        else if (weapons.Count > 0 && Input.GetMouseButtonUp(0))
        {
            if (isRocketLauncherEquipped && isAiming)
            {
                targetPosition = GetMouseWorldPosition();
                FireRocket();
                isAiming = false;
            }
            else if (!isRocketLauncherEquipped)
            {
                StopShooting();
            }
        }

        if (weapons.Count > 0 && Input.GetMouseButtonDown(1))
        {
            ChangeWeaponIndex(selectedWeaponIndex + 1);
        }
    }

    private void CheckIfRocketLauncherEquipped()
    {
        if (weapons.Count > 0)
        {
            isRocketLauncherEquipped = weapons[selectedWeaponIndex] is RocketLauncher;
        }
    }

    public void SelectLatesWeapon()
    {
        ChangeWeaponIndex(selectedWeaponIndex + 1);
    }

    private void ChangeWeaponIndex(int index)
    {
        selectedWeaponIndex = index;
        if (selectedWeaponIndex >= weapons.Count)
        {
            selectedWeaponIndex = 0;
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeaponIndex)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }

        CheckIfRocketLauncherEquipped(); 
    }

    private void Movement()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputVector = inputVector.normalized;

        if (canMove)
        {
            float currentSpeed = speed;
            if (_anim.GetBool("running"))
            {
                currentSpeed *= runSpeedMultiplier; 
            }

            if (isRocketLauncherEquipped)
            {
                currentSpeed *= 0.6f;
            }

            _rb.velocity = inputVector * currentSpeed;

            if (inputVector.magnitude > 0)
            {
                _anim.SetBool("walking", true);
                wasMoving = true; 
            }
            else
            {
                _anim.SetBool("walking", false);
                wasMoving = false;
            }
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _anim.SetBool("walking", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            StartRunning();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopRunning();
        }
    }

    private void RotateTowardsMouse()
    {
        Ray rayFromCameraCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraCursor, out float distanceFromCamera);
        cursorPosition = rayFromCameraCursor.GetPoint(distanceFromCamera);

        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray rayFromCameraCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraCursor, out float distanceFromCamera);
        return rayFromCameraCursor.GetPoint(distanceFromCamera);
    }

    private void FireRocket()
    {
        if (weapons.Count > 0)
        {
            Weapon selectedWeapon = weapons[selectedWeaponIndex];
            if (selectedWeapon is RocketLauncher)
            {
                RocketLauncher rocketLauncher = (RocketLauncher)selectedWeapon;
                rocketLauncher.FireRocket(targetPosition);
            }
        }
    }

    private void StartShooting()
    {
        _anim.SetBool("shooting", true);
        weapons[selectedWeaponIndex].Fire(cursorPosition);

        if (!wasMoving)
        {
            canMove = false; 
        }
    }

    private void StopShooting()
    {
        _anim.SetBool("shooting", false);
        canMove = true; 
    }

    private void StartRunning()
    {
        _anim.SetBool("running", true);
    }

    private void StopRunning()
    {
        _anim.SetBool("running", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon therWeapon = other.GetComponentInParent<Weapon>();
        WeaponRotator rotator = other.GetComponent<WeaponRotator>();

        if (therWeapon != null)
        {
            weapons.Add(therWeapon);
            therWeapon.transform.position = weaponPos.transform.position;
            therWeapon.transform.rotation = weaponPos.transform.rotation;
            therWeapon.transform.SetParent(weaponPos.transform);

            Destroy(rotator);

            ChangeWeaponIndex(weapons.Count - 1);
        }

        if (other.CompareTag("key"))
        {
            Reference.isKey = true;
            Destroy(other.gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {

        GameObject otherGameObject = collision.gameObject;


        if (otherGameObject.CompareTag("BulletEnemy"))
        {

            Bullet otherBullet = otherGameObject.GetComponent<Bullet>();

            if (otherBullet != null)
            {
                _healthSystem.TakeDamage(otherBullet.damage);
            }
        }

    }


}
