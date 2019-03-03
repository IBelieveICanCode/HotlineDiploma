using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion: MonoBehaviour
{
    public float damage;

    void OnParticleCollision(GameObject other)
    {
        Destructable target = other.GetComponent<Destructable>();
        if (target != null)
        {
            target.Hit(damage);
        }
    }

}
