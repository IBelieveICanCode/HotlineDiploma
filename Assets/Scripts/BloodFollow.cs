using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BloodFollow : MonoBehaviour
{
    private Transform target;
    int num_activeParticles;

    ParticleSystem allParticles;
    ParticleSystem.Particle[] bloodParticles;
    private void Start()
    {
        allParticles = GetComponent<ParticleSystem>();
        Setup();
    }

    private void LateUpdate()
    {
        num_activeParticles = allParticles.GetParticles(bloodParticles);
        StartCoroutine(Timer());
        

    }

    private void OnParticleCollision(GameObject player)
    {
        if (player.GetComponent<PlayerControl>())
        {
            print("blood +1");
            
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        GatherBlood();
    }

    private void GatherBlood()
    {
        for (int i = 0; i < num_activeParticles; i++)
        {
            target = FindObjectOfType<PlayerControl>().transform;
            bloodParticles[i].velocity = Vector3.zero;
            //bloodParticles[i].velocity = target.position;
            bloodParticles[i].position = Vector3.Lerp(bloodParticles[i].position, target.position, 0.01f);
        }
       allParticles.SetParticles(bloodParticles, num_activeParticles);
    }

    private void Setup()
    {
        bloodParticles = new ParticleSystem.Particle[allParticles.maxParticles];

    }
}
