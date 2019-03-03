using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BloodFollow : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    float speed = 10;
    int num_particles;

    ParticleSystem allParticles;
    ParticleSystem.Particle[] bloodParticles = new ParticleSystem.Particle[1000];
    private void Start()
    {
        allParticles = GetComponent<ParticleSystem>();
        
        
    }

    private void LateUpdate()
    {
        num_particles = allParticles.GetParticles(bloodParticles, allParticles.particleCount);
        GatherBlood();
        

    }

 

    private void OnParticleCollision(GameObject player)
    {
        if (player.GetComponent<PlayerControl>())
        {
            print("blood +1");
            
        }
    }

    private void GatherBlood()
    {
        
        for (int i = 0; i < num_particles; i++)
        {
            bloodParticles[i].position = Vector3.MoveTowards(bloodParticles[i].position, target.position, speed * Time.deltaTime);
            
        }
        //allParticles.SetParticles(bloodParticles, num_particles);
    }
}
