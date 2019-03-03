
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgun : Weapon
{
    [SerializeField]
    [Range(0.1f, 1f)]
    float range = 0.1f;
    [SerializeField]
    private float shellAmount;



    public override void Use()
    {
        
        for (int i = 0; i < shellAmount; i++)
        {
            MenuAudioController.Instance.PlaySound("shotgunshot", false);
            GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            float xEuler = Random.Range(-range, range);
            //float YEuler = Random.Range(-maxAlpha, maxAlpha);
            //newBullet.transform.eulerAngles = new Vector3(xEuler, YEuler, newBullet.transform.rotation.z);
            newBullet.GetComponent<Rigidbody>().AddForce((muzzle.forward + muzzle.right * xEuler).normalized * shootPower);
            newBullet.GetComponent<Damager>().Damage = damage;
            Destroy(newBullet, 5);
        }
    }
}
