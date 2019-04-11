using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ObstacleDestructabel : ParticleDestructable
{
    private Material obstacleMaterial;

    protected void Awake()
    {
        base.Awake();
        obstacleMaterial = GetComponent<MeshRenderer>().material;
    }

    protected override void SpawnParticle()
    {
        GameObject _obstacleParticles = Instantiate(particleSystem.gameObject, new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z), Quaternion.identity) as GameObject;
        Color color = new Color(obstacleMaterial.color.r, obstacleMaterial.color.g, obstacleMaterial.color.b, 1f);
        _obstacleParticles.GetComponent<ParticleSystem>().startColor = color;
        _obstacleParticles.GetComponent<ParticleSystem>().Play();
        MenuAudioController.Instance.PlaySound("rubble", false);
        float _duration =  _obstacleParticles.GetComponent<ParticleSystem>().main.duration;
        Destroy(_obstacleParticles, _duration);
    }
}
