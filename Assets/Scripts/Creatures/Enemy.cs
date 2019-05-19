using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Transform target;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private GameObject enemyWeapon;
    [SerializeField]
    private Transform weaponHolder;
    private Destructable destructable;

    [SerializeField]
    float nextShootTime = 1;
//TODO: Rewardable

    private bool seeTarget = true;

    void Start()
    {
        target = FindObjectOfType<PlayerParticleDestructable>().transform;
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        destructable = GetComponent<Destructable>();
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
            CheckTargetVisibility();
        }
        
         Shoot();
        
        //transform.forward = Camera.main.transform.position - transform.position;
    }

    private void Shoot()
    {
        //if (seeTarget == true && Time.time > nextShootTime)
        if (Time.time > nextShootTime)
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


}
