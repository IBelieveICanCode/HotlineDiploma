using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    public override void Use()
    {
        base.Use();
        MenuAudioController.Instance.PlaySound("rocketlauncher", false);
        GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        //newBullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * shootPower);
        newBullet.GetComponent<Damager>().Damage = damage;
        Destroy(newBullet, 5);
    }
    
}
