using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public override void Use()
    {
        base.Use();
        MenuAudioController.Instance.PlaySound("machinegun", false);
        GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * shootPower);
        //print(newBullet.GetComponent<Rigidbody>().velocity);
        newBullet.GetComponent<Damager>().Damage = damage;
        Destroy(newBullet, 5);
    }
}
