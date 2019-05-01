using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Destructable : MonoBehaviour,IDestructable,ILogicDeathDependable
{
    //[HideInInspector]
    //public bool isRewarded;
    //[HideInInspector]
    //public List<Reward> rewards = new List<Reward>();
    public bool devMode = false;
    [SerializeField]
    protected float maxHealth = 100f;
    protected float currentHealth;
    float IDestructable.CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    float IDestructable.MaxHealth { get { return maxHealth;} }
    protected OnDeathHandler deathEvent;

    event OnDeathHandler ILogicDeathDependable.DeathEvent
    {
        add { deathEvent += value; }
        remove { deathEvent -= value; }
    }

    protected void Awake () {
        Init();
     }

    protected virtual void Init()
    {
        currentHealth = maxHealth;
        //if (isRewarded)
            //deathEvent += GiveReward;
    }

    /*
    private void GiveReward()
    {
        
    }
    */
    protected void Die()
    {
        if (deathEvent != null)
        {
            deathEvent();
            deathEvent = null;
            Destroy(gameObject);
        }
       
    }

    void IDestructable.ReceiveHit(float damage)
    {
        if (!devMode)
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
}
