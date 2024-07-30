using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyController
{
    [SerializeField] private GameObject jumpIndicatorPrefab;
    [SerializeField] private List<Weapon> myWeapon;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float warningDuration;
    [SerializeField] private float angerDuration; 
    [SerializeField] private float angerCooldown;

    private Vector3 jumpTargetPosition;
    private GameObject currentIndicator;
    private bool isAngry = false;
    private bool isPreparingToJump = false;
    private float angerTimer = 0f; 

    

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Taunt());
        StartCoroutine(ManageAnger());
        _animator.SetBool("Idle", true);

       
    }

    protected override void Update()
    {
        DetectPlayer();
        if (isAngry)
        {
            angerTimer += Time.deltaTime;
            if (angerTimer >= angerDuration)
            {
                isAngry = false;
                angerTimer = 0f;
                _animator.SetBool("BigShot", false);
            }
        }
    }

    public void HandleDeath()
    {
        if (_animator != null)
        {
            _animator.SetBool("Died", true);
        }

        // G?i ShowWinPanel khi boss ch?t
        GameManager.instance.ShowWinPanel();
    }


    public void Appear()
    {
        gameObject.SetActive(true); 
    }

    public void DetectPlayer()
    {
        if (Reference.thePlayer != null)
        {
            if (IsPlayerInAttackRange())
            {
                _agent.isStopped = true;
                if (!isPreparingToJump) 
                {
                    if (isAngry)
                    {
                        _animator.SetBool("BigShot", true);
                        FireAllWeapons();
                        
                    }
                    FireSingleWeapon();
                    _animator.SetBool("Shot", true);
                }
                transform.LookAt(PlayerPosition());
            }
            else
            {
                _agent.isStopped = false;
                _animator.SetBool("Idle", true);
                _animator.SetBool("Shot", false);
                _animator.SetBool("BigShot", false);
            }
        }
        else
        {
            _animator.SetBool("Idle", true);
            _animator.SetBool("Shot", false);
            _animator.SetBool("BigShot", false);
        }
    }

    private void FireSingleWeapon()
    {
        if (myWeapon.Count > 0)
        {
            myWeapon[0].Fire(PlayerPosition());
        }
    }

    private void FireAllWeapons()
    {
        foreach (Weapon weapon in myWeapon)
        {
            weapon.Fire(PlayerPosition());
        }
    }

    private IEnumerator Taunt()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpCooldown);

            if (IsPlayerInAttackRange())
            {
                Vector3 playerPosition = PlayerPosition();
                ShowJumpWarning(playerPosition);
                

                yield return new WaitForSeconds(warningDuration);

                isPreparingToJump = true;
                _animator.SetBool("Taunt", true);
                yield return StartCoroutine(JumpToPlayer());
                isPreparingToJump = false;
                _animator.SetBool("Taunt", false);
            }
        }
    }

    private IEnumerator JumpToPlayer()
    {
        Vector3 startPosition = transform.position;
        float jumpDuration = .8f;
        float elapsedTime = 0;

        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
        }

        while (elapsedTime < jumpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / jumpDuration;

            Vector3 currentPosition = Vector3.Lerp(startPosition, jumpTargetPosition, t);
            currentPosition.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

            transform.position = currentPosition;

            yield return null;
        }
    }

    private void ShowJumpWarning(Vector3 position)
    {
        if (jumpIndicatorPrefab != null)
        {
            currentIndicator = Instantiate(jumpIndicatorPrefab, position + transform.up * 0.2f, Quaternion.Euler(90f, 0f, 0f));
            jumpTargetPosition = currentIndicator.transform.position;
        }
    }

    public Vector3 PlayerPosition()
    {
        return Reference.thePlayer.transform.position;
    }

    private IEnumerator ManageAnger()
    {
        while (true)
        {
            yield return new WaitForSeconds(angerCooldown);
            isAngry = true;
        }
    }



}
