using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMachineGun : Weapon
{
    public override void Use()
    {
        MenuAudioController.Instance.PlaySound("anothermachinegun", false);
        GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * shootPower);
        //print(newBullet.GetComponent<Rigidbody>().velocity);
        if (newBullet.GetComponent<Damager>().target == FindObjectOfType(typeof(PlayerControl)))
            //newBullet.GetComponent<Damager>().Damage = damage;
        Destroy(newBullet, 5);
    }
}

