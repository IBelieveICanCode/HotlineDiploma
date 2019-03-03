using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    private NavMeshAgent _navMeshAgent;

    public Transform target;

    [SerializeField]
    private GameObject medkit;

    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private GameObject enemyWeapon;
    [SerializeField]
    private Transform weaponHolder;

   

    [SerializeField]
    float nextShootTime = 1;
    public float reward;

    private bool seeTarget = true;


    private void Awake()
    {
        
    }

    void Start()
    {
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        Destructable enemy = GetComponent<Destructable>();

        enemy.OnDeath += EnemyDied;
        GetWeapon(enemyWeapon);
        
    }
    
    private void GetWeapon(GameObject newWeapon)
    {
        GameObject defaultWeapon = Instantiate(newWeapon, weaponHolder.position, gameObject.transform.rotation);
        defaultWeapon.transform.parent = weaponHolder;
        weapon = defaultWeapon.GetComponent<Weapon>();
    }
    

    // Update is called once per frame
    void Update ()
    {

        //target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target != null)
        {
            _navMeshAgent.SetDestination(target.position);
            animator.SetFloat("velocity", _navMeshAgent.speed);
            CheckTargetVisibility();
        }
        
         Shoot();
        
        //transform.forward = Camera.main.transform.position - transform.position;
    }

    private void Shoot()
    {
        if (seeTarget == true && Time.time > nextShootTime)
        {
            
            weapon.Use();

            nextShootTime = Time.time + weapon.executionDelay;
            
        }
    }

    private void CheckTargetVisibility()
    {
        Vector3 targetDirection = target.position - weaponHolder.transform.position;

        Ray ray = new Ray(weaponHolder.transform.position, targetDirection);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == target)
            {
                seeTarget = true;
                return;
            }
        }

        seeTarget = false;
    }

    private void EnemyDied()
    {
        if (UnityEngine.Random.Range(0, 100) < 20)
        {
            HealthBonus.Create(transform.position);
        }        else if (UnityEngine.Random.Range(0, 100) > 80)
        {
                Ammo.Create(transform.position);
        }        MenuAudioController.Instance.PlaySound("tearingflesh", false);
        HUD.Score += reward;

    }
}
