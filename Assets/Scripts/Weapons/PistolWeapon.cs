using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : Weapon
{
    public ParticleSystem explosion;
    /*void IShooting.Shoot()
    { 
        GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * shootPower);
        newBullet.GetComponent<Damager>().Damage = damage;
        Destroy(newBullet, 5);
       
    }*/
    //public override void Hide()

    public override void Use()
    {
        MenuAudioController.Instance.PlaySound("pistolshot", false);
        GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * shootPower);
        newBullet.GetComponent<Damager>().Damage = damage;
        Destroy(newBullet, 5);
        
    }


}
