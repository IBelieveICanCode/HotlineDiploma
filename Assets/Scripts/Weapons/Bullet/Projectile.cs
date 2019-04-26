using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Damager
{ 
    [SerializeField]
    protected GameObject afterEffectParticle;

    protected virtual void OnDestroy()
    {
        GameObject bulletSpark = Instantiate(afterEffectParticle, transform.position, Quaternion.identity);       
    }
}
