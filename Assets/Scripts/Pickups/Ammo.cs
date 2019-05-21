using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
        if (player)
        {
            player.ChooseShootWeapon.CurrentWeapon.ammo = player.ChooseShootWeapon.CurrentWeapon.maxAmmo;
            Destroy(gameObject);
        }
    }
}