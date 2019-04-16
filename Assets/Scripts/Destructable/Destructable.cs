using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Destructable : MonoBehaviour,IDestructable,ILogicDeathDependable
{
    [SerializeField]
    protected float maxHealth = 100f;
    protected float currentHealth;
    float IDestructable.CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    float IDestructable.MaxHealth { get { return maxHealth;} }
    private OnDeathHandler deathEvent;

    event OnDeathHandler ILogicDeathDependable.DeathEvent
    {
        add { deathEvent += value; }
        remove { deathEvent -= value; }
    }

    protected void Awake () {
        Init();
     }

    private void Init()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Die()
    {
        if (deathEvent != null)
            deathEvent();
        Destroy(gameObject);
    }

    void IDestructable.ReceiveHit(float damage)
    {
        //if (gameObject.tag == "Player")
            //print("In destructabel current health " + currentHealth);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
