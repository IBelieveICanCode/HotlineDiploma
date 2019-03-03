using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeDamager : MonoBehaviour
{
    [SerializeField]
    float timer;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    float explosionDamage;

    private void Start()
    {
        //explosionDamage = Damage;
//Damage = 0.1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
            Destroy(gameObject);
    }
    private void OnDestroy()
    {
        MenuAudioController.Instance.PlaySound("explosion", false);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionDamage = explosion.GetComponent<Explosion>().damage;
        float deathTime = explosion.GetComponent<ParticleSystem>().main.duration;
        Destroy(explosion, deathTime);
    }
}
