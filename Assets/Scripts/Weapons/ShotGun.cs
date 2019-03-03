
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    [SerializeField]
    [Range(0.1f, 1f)]
    float range = 0.1f; 
    [SerializeField]
    private float shellAmount;


    
public override void Use()
{
        
        base.Use();
    for (int i = 0; i < shellAmount; i++)
    {
            MenuAudioController.Instance.PlaySound("shotgun", false);
        GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        float xEuler = Random.Range(-range, range);
        //float YEuler = Random.Range(-maxAlpha, maxAlpha);
        //newBullet.transform.eulerAngles = new Vector3(xEuler, YEuler, newBullet.transform.rotation.z);
        newBullet.GetComponent<Rigidbody>().AddForce((muzzle.forward + muzzle.right * xEuler).normalized * shootPower);
        newBullet.GetComponent<Damager>().Damage = damage;
        Destroy(newBullet, 5);
    }
}



        /*
    [SerializeField]
    private int _shellAmount;
    [SerializeField]
    private float _angle;
    [SerializeField]
    List<Quaternion> _shells;

    private void Awake()
    {
        _shells = new List<Quaternion>(_shellAmount);
        for (int i = 0; i < _shellAmount; i++)
        {
            _shells.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    public override void Use()
    {
        base.Use();
        int i = 0;
        foreach (Quaternion quat in _shells)
        {
            _shells[i] = Random.rotation;
            GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            newBullet.transform.rotation = Quaternion.RotateTowards(newBullet.transform.rotation, _shells[i], _angle);
            newBullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * shootPower);
            newBullet.GetComponent<Damager>().Damage = damage;
            i++;
        }
    }
    */
}