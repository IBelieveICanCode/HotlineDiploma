using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfWeapon {Pistol, MachineGun, Shotgun, RocketLauncher, GrenadeLauncher, LaserGun}
[System.Serializable]
public abstract class Weapon: MonoBehaviour
{
    public TypeOfWeapon TypeOfWeapon;
    [Header("Basic Weapon Characteristics")]
    public Sprite WeaponSprite;
    [SerializeField]
    protected float damage;
    public float executionDelay;
    [SerializeField]
    protected float shootPower;

    public bool infiniteAmmo;
    public int maxAmmo;
    public int ammo;
    [SerializeField]
    protected GameObject bulletPrefab;

    [SerializeField]
    protected Transform muzzle;


    protected void outOfAmmo()
    {
        ammo--;  
        if (ammo <= 0)
        {
            FindObjectOfType<PlayerControl>().ChooseShootWeapon.RemoveWeaponFromArsenal(gameObject);
        }
    }

    //public abstract void Hide();
    public virtual void Use() {
        if (!infiniteAmmo)
            outOfAmmo();
    }
}
