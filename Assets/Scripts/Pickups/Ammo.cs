using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
   // Weapon thisWeapon; 
    void OnTriggerEnter(Collider other)
    {
        PlayerControl thisWeapon = other.gameObject.GetComponent<PlayerControl>();
        if (thisWeapon != null)
        {
            MenuAudioController.Instance.PlaySound("ammo", false);
            thisWeapon.CurrentWeapon.ammo = thisWeapon.CurrentWeapon.maxAmmo;
            Destroy(gameObject);
        }
    }

    public static void Create(Vector3 position)
    {
        Instantiate(Resources.Load("Prefabs/AmmoBoxPivot"), position, Quaternion.identity);
    }
}
