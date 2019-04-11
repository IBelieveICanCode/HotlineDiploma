using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion: MonoBehaviour
{
    public float damage;

    void OnParticleCollision(GameObject other)
    {
        IDestructable target = other.GetComponent<IDestructable>();
        if (target != null)
        {
            target.ReceiveHit(damage);
        }
    }

}
