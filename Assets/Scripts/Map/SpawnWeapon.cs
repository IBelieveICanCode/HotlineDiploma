using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    
    [SerializeField]
    private List<TriggerWeapon> myWeapons = new List<TriggerWeapon>();

    MapGenerator map;
    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<MapGenerator>();
        WeaponAppear(1);
        FindObjectOfType<Spawner>().OnNewWave += WeaponAppear ;
    }

    // Update is called once per frame


    public void WeaponAppear(int a)
    {
        TriggerWeapon[] tw = FindObjectsOfType<TriggerWeapon>();
        foreach (TriggerWeapon t in tw)
        {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < myWeapons.Count; i++)
        {
            Transform randomTile = map.GetRandomOpenTile();
            TriggerWeapon weaponTrigger = Instantiate(myWeapons[i], randomTile.position + Vector3.up, myWeapons[i].transform.rotation);
        }

    }
}
