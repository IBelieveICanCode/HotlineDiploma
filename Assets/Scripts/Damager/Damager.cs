using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damager : MonoBehaviour {

    public IDestructable target;
    public float Damage;


    private void OnCollisionEnter(Collision collision)
    {
        target = collision.gameObject.GetComponent<IDestructable>();
        if (target != null)
        {
            target.ReceiveHit(Damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        TrailEffectDie();
    }

    /*otected virtual void DieEffect()
    { 
        Destroy(gameObject);
    }
    */
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
}
