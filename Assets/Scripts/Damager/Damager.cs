using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damager : MonoBehaviour {

    public Destructable target;
    
    public float Damage;


    private void OnCollisionEnter(Collision collision)
    {
        target = collision.gameObject.GetComponent<Destructable>();
        if (target != null)
        {
            target.Hit(Damage);
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
