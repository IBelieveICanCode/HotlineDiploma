using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleDestructable : ParticleDestructable
{
    [SerializeField]
    private float RewardPoints;
    protected override void Init()
    {
        base.Init();
        deathEvent += SpawnBonus;
        deathEvent += GivePoints;
    }

    private void SpawnBonus()
    {
        EnemyBonusSpawner.SpawnEnemyBonus(transform.position);
    }
    private void GivePoints()
    {
        HUDScoresScript.Score += RewardPoints;
    }
}
