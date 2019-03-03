using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Weapon: MonoBehaviour
{
    [Header("Basic Weapon Characteristics")]
    public Sprite WeaponSprite;
    [SerializeField]
    protected float damage;
    public float executionDelay;
    [SerializeField]
    protected float shootPower;
    
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
            FindObjectOfType<PlayerControl>().RemoveWeaponFromArsenal(gameObject);
        }
    }

    //public abstract void Hide();
    public virtual void Use() {
        outOfAmmo();
    }
}
