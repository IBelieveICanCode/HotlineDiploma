using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesRemoval : MonoBehaviour
{
    Material obstacleMaterial;

    [SerializeField]
    ParticleSystem rubbles;

    private void Awake()
    {
        
    }
    private void Start()
    {
        obstacleMaterial = GetComponent<MeshRenderer>().material;
        Destructable target = GetComponent<Destructable>();
        //print(obstacleMaterial.color);
        target.OnDeath += Rubbles;
    }
    private void Rubbles()
    {

        GameObject myRubbles = Instantiate(rubbles.gameObject, new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z), Quaternion.identity) as GameObject;
        Color color = new Color(obstacleMaterial.color.r, obstacleMaterial.color.g, obstacleMaterial.color.b, 1f);
        myRubbles.GetComponent<ParticleSystem>().startColor = color;
        myRubbles.GetComponent<ParticleSystem>().Play();
        MenuAudioController.Instance.PlaySound("rubble", false);
        Destroy(myRubbles, myRubbles.GetComponent<ParticleSystem>().main.duration);
    }
}
