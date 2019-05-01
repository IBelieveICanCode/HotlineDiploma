using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ObstacleDestructabel : ParticleDestructable
{
    private Renderer obstacleRenderer;

    protected override void Init()
    {
        base.Init();
        obstacleRenderer = GetComponent<Renderer>();
    }

    protected override void SpawnParticle()
    {
        //TODO: Fix size
        GameObject _obstacleParticles = Instantiate(particleSystem.gameObject, new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z), Quaternion.identity) as GameObject;
        Color color = new Color(obstacleRenderer.material.color.r, obstacleRenderer.material.color.g, obstacleRenderer.material.color.b, 1f);
        var main = _obstacleParticles.GetComponent<ParticleSystem>().main;
        main.startColor = color;
        _obstacleParticles.GetComponent<ParticleSystem>().Play();
        MenuAudioController.Instance.PlaySound("rubble", false);
        float _duration =  _obstacleParticles.GetComponent<ParticleSystem>().main.duration;
        Destroy(_obstacleParticles, _duration);
    }
}
