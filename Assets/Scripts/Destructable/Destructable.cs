using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Destructable : MonoBehaviour,IDestructable
{
    [SerializeField]
    protected float maxHealth = 100f;
    protected float currentHealth;
    float IDestructable.CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    float IDestructable.MaxHealth { get { return maxHealth;} }

    protected void Awake () {
        Init();
     }

    private void Init()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    void IDestructable.ReceiveHit(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
