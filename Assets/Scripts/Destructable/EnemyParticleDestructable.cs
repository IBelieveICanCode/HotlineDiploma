using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleDestructable : ParticleDestructable
{
    protected override void Die()
    {
        EnemyBonusSpawner.SpawnEnemyBonus(transform.position);
    }
}
