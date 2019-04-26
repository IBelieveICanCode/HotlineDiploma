using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Damager : MonoBehaviour {

    public IDestructable target;
    public float Damage;


    protected virtual void OnCollisionEnter(Collision collision)
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
    }
}
