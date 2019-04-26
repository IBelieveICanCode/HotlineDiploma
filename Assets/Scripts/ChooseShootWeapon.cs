using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChooseShootWeapon : MonoBehaviour
{
    [SerializeField]
    protected Transform _weaponHolder;

    public List<GameObject> Arsenal = new List<GameObject>();
    public Weapon CurrentWeapon;
    public GameObject Pistol;
    protected float _nextShootTime = 0;

    protected void UseWeapon()
    {
            if (Time.time > _nextShootTime)
            {
                CurrentWeapon.Use();
                _nextShootTime = Time.time + CurrentWeapon.executionDelay;
            }    
    }

    protected void PickUpWeapon()
    {
        Collider[] findTriggerWeapon = Physics.OverlapSphere(transform.position, 1.5f);
        for (int i = 0; i < findTriggerWeapon.Length; i++)
        {
            TriggerWeapon newWeapon = findTriggerWeapon[i].GetComponent<TriggerWeapon>();
            if (newWeapon && Input.GetKeyUp(KeyCode.Mouse1))
            {
                if (!CheckIfWeaponInList(newWeapon))
                    GetWeapon(newWeapon.triggerWeapon);
                else
                {
                    WeaponScroll(Arsenal.IndexOf(newWeapon.triggerWeapon));
                    Arsenal[Arsenal.IndexOf(newWeapon.triggerWeapon)].GetComponent<Weapon>().ammo = Arsenal[Arsenal.IndexOf(newWeapon.triggerWeapon)].GetComponent<Weapon>().maxAmmo;
                }
                Destroy(newWeapon.gameObject);
            }
        }
    }

    protected bool CheckIfWeaponInList(TriggerWeapon newWeapon)
    {
        if (Arsenal.Contains(newWeapon.triggerWeapon))
            return true;
        else
            return false;
    }

    protected void WeaponScroll(int n)
    {
        foreach (GameObject w in Arsenal)
        {
            w.SetActive(false);
        }
        Arsenal[n].SetActive(true);
        CurrentWeapon = Arsenal[n].GetComponent<Weapon>();
        HUD.Instance.ChangeWeaponImage(Arsenal[n].GetComponent<Weapon>().WeaponSprite);
    }


    public void RemoveWeaponFromArsenal(GameObject removedWeapon)
    {
        Arsenal.Remove(CurrentWeapon.gameObject as GameObject);
        WeaponScroll(0);
    }



    protected void GetWeapon(GameObject newWeapon)
    {
        MenuAudioController.Instance.PlaySound("ammo", false);
        foreach (GameObject g in Arsenal)//Transform child in _weaponHolder)
        {
            g.SetActive(false);
        }

        GameObject defaultWeapon = Instantiate(newWeapon, _weaponHolder.position, gameObject.transform.rotation);

        Arsenal.Add(defaultWeapon);
        defaultWeapon.transform.parent = _weaponHolder;
        CurrentWeapon = defaultWeapon.GetComponent<Weapon>();
        HUD.Instance.ChangeWeaponImage(defaultWeapon.GetComponent<Weapon>().WeaponSprite);
    }

    public void GetPistol()
    {
        GetWeapon(Pistol);
    }

    protected void ChooseWeapon(float scroll)
    {
        float scrollDir = scroll / Mathf.Abs(scroll);
        int currentWeaponIndex = Arsenal.IndexOf(CurrentWeapon.gameObject);
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
