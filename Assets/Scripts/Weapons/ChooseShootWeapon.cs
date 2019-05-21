using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChooseShootWeapon : MonoBehaviour
{
    [SerializeField]
    protected Transform _weaponHolder;

    private List<Weapon> Arsenal = new List<Weapon>();
    public Weapon CurrentWeapon;
    public Weapon Pistol;
    protected float _nextShootTime = 0;

    public void UseWeapon()
    {
        if (Time.time > _nextShootTime)
        {
            CurrentWeapon.Use();
            _nextShootTime = Time.time + CurrentWeapon.executionDelay;
        }    
    }

    public void PickUpWeapon()
    {
        Collider[] collidersAround = Physics.OverlapSphere(transform.position, 1.5f);
        List<TriggerWeapon> triggersList = new List<TriggerWeapon>();
        foreach (Collider col in collidersAround)
        {
            if (col.gameObject.GetComponent<TriggerWeapon>() != null)
            {
                triggersList.Add(col.gameObject.GetComponent<TriggerWeapon>());
            }
        }

        if (triggersList.Count == 0)
        {
            return;
        }

        //for (int i = 0; i < findTriggerWeapon.Length; i++)
        //{
        TriggerWeapon triggerWeapon = triggersList[0].GetComponent<TriggerWeapon>();
        Weapon newWeapon = triggerWeapon.Weapon;

        if (newWeapon && Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (!CheckIfWeaponInList(newWeapon))
            {
                GetWeapon(newWeapon);
            }
            else
            {
                foreach (Weapon weapon in Arsenal)
                {
                    if (weapon.TypeOfWeapon == newWeapon.TypeOfWeapon)
                    {
                        WeaponScroll(Arsenal.IndexOf(weapon));
                        Arsenal[Arsenal.IndexOf(weapon)].ammo = Arsenal[Arsenal.IndexOf(weapon)].maxAmmo;
                    }
                }
            }
            Destroy(triggersList[0].gameObject);
        }

        //}
    }

    private bool CheckIfWeaponInList(Weapon newWeapon)
    {
        foreach (Weapon weapon in Arsenal)
        {
            if (weapon.TypeOfWeapon == newWeapon.TypeOfWeapon)
            {
                return true;
            }
        }
        return false;
    }

    private void WeaponScroll(int n)
    {
        foreach (Weapon w in Arsenal)
        {
            w.gameObject.SetActive(false);
        }
        Arsenal[n].gameObject.SetActive(true);
        CurrentWeapon = Arsenal[n].GetComponent<Weapon>();
        HUD.Instance.ChangeWeaponImage(Arsenal[n].WeaponSprite);
    }


    public void RemoveWeaponFromArsenal(GameObject removedWeapon)
    {
        Arsenal.Remove(CurrentWeapon);
        WeaponScroll(0);
    }



    private void GetWeapon(Weapon newWeapon)
    {
        MenuAudioController.Instance.PlaySound("ammo", false);
        foreach (Weapon g in Arsenal)//Transform child in _weaponHolder)
        {
            g.gameObject.SetActive(false);
        }

        Weapon defaultWeapon = Instantiate(newWeapon, _weaponHolder.position, gameObject.transform.rotation);

        Arsenal.Add(defaultWeapon);
        defaultWeapon.transform.parent = _weaponHolder;
        CurrentWeapon = defaultWeapon;
        HUD.Instance.ChangeWeaponImage(defaultWeapon.WeaponSprite);
    }

    public void GetPistol()
    {
        GetWeapon(Pistol);
    }

    public void ChooseWeapon(float scroll)
    {
        float scrollDir = scroll / Mathf.Abs(scroll);
        int currentWeaponIndex = Arsenal.IndexOf(CurrentWeapon);
        int number = currentWeaponIndex + (int)scrollDir;
        if (number < Arsenal.Count && number >= 0)
        {
            WeaponScroll(number);
        }
        else if (number < 0)
        {
            WeaponScroll(Arsenal.Count - 1);
        }
        else if (number >= Arsenal.Count)
        {
            WeaponScroll(0);
        }
    }
}
