using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    public override void Use()
    {
        base.Use();
        MenuAudioController.Instance.PlaySound("grenadelauncher", false);
        GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * shootPower);
        //newBullet.GetComponent<Damager>().Damage = damage;
        Destroy(newBullet, 1.5f); //TODO timer for bullets
    }
}
