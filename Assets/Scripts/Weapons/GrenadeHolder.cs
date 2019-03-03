using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeHolder : Weapon
{
    public override void Use()
    {
        //base.Use();
        if (ammo > 0)
        {
            ammo--;
            MenuAudioController.Instance.PlaySound("grenadethrow", false);
            GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * shootPower);
            //newBullet.GetComponent<Damager>().Damage = damage;
            Destroy(newBullet, 1f);
            
        }
        
    }
}
