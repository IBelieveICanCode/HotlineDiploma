using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAmmoHUD : MonoBehaviour
{
    public Text _ammoCount;
    public Image TypeWeapon;


    public void SetAmmoCount(float ammo)
    {
        _ammoCount.text = ammo.ToString();
    }
    public void ChangeWeaponImage(Sprite weapon)
    {
        TypeWeapon.sprite = weapon;
    }

}
