using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion: MonoBehaviour
{
    public float Damage = 10;
    void OnParticleCollision(GameObject other)
    {
        IDestructable target = other.GetComponent<IDestructable>();
        if (target != null)
        {
            target.ReceiveHit(Damage);
        }
    }

}
