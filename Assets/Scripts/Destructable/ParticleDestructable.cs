using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleDestructable : Destructable
{
    [SerializeField]
    protected GameObject particleSystem;

    protected override void Init()
    {
        base.Init();
        deathEvent += SpawnParticle;
    }

    protected virtual void SpawnParticle()
    {
        GameObject _partice = Instantiate(particleSystem, new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z), Quaternion.identity);
        float _deathTime = _partice.GetComponentInChildren<ParticleSystem>().main.duration;
        Destroy(_partice, _deathTime);
    }
}
