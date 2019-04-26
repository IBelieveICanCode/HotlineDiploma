using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectiles : Damager
{

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        //TrailEffectDie();
    }

    /*
    void TrailEffectDie()
    {
        ParticleSystem trail = gameObject.GetComponentInChildren<ParticleSystem>();
        if (trail != null)
        {
            Destroy(trail.gameObject, trail.startLifetime);
            trail.Stop();
            trail.transform.SetParent(null);
        }
    }
    */
}
