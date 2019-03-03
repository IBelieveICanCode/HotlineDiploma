using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroUpgrades
{
    //[SerializeField]
    //private float maxHealth = 100;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float _dashDistance = 60f;
    private List<Weapon> weapons;

    //public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Speed { get => speed; set => speed = value; }
    public List<Weapon> Weapons { get => weapons; set => weapons = value; }
    public float DashDistance { get => _dashDistance; set => _dashDistance = value; }
}
