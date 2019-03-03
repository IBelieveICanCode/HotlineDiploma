using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    private GameObject blood;

    public float hitPoints = 100.0f;

    public float hitPointsCurrent;

    public event System.Action OnDeath;
    // Use this for initialization
    void Awake () {
        hitPointsCurrent = hitPoints;
        if (blood != null)
        {
            OnDeath += isBlood;
        }
     }

    private void isBlood()
    {
        GameObject thisBlood = Instantiate(blood, new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z), Quaternion.identity);
        float deathTime = thisBlood.GetComponentInChildren<ParticleSystem>().main.duration;
        Destroy(thisBlood, deathTime);
    }
    public void Hit(float damage)
    {
        hitPointsCurrent -= damage;
        if (gameObject.GetComponent<PlayerControl>() != null)
        {
            HUD.Instance.HealthBar.value = hitPointsCurrent;
        }
        if (hitPointsCurrent <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (OnDeath != null)
        {
            OnDeath();
            OnDeath = null;
        }
        GameObject.Destroy(gameObject);
    }
}
