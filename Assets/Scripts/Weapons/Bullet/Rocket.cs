using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField]
    protected float explosionDamage;

    protected override void OnDestroy()
    {
        MenuAudioController.Instance.PlaySound("explosion", false);
        GameObject explosion = Instantiate(afterEffectParticle, transform.position, Quaternion.identity);
        Explosion explosionLogic = explosion.GetComponent<Explosion>();
        if (explosionLogic != null)
        {
            explosionLogic.Damage = explosionDamage;
        }
        float deathTime = explosion.GetComponent<ParticleSystem>().main.duration;
        Destroy(explosion, deathTime);
    }
}
