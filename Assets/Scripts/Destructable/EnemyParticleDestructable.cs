using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleDestructable : ParticleDestructable
{
    protected override void Init()
    {
        base.Init();
        deathEvent += SpawnBonus;
    }

    private void SpawnBonus()
    {
        EnemyBonusSpawner.SpawnEnemyBonus(transform.position);
    }
}
