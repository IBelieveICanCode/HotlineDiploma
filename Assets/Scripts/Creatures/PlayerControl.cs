using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (CharacterController))]
public class PlayerControl: PlayerBasicControl
{
    public ChooseShootWeapon ChooseShootWeapon;
    [SerializeField]
    private Transform _dashEffect;
    private float _nextDashTime = 0;
    [SerializeField]
    private float _dashDelay;
    [SerializeField]
    private float _dashDistance = 5f;
    
    
    void Awake ()
    {
        Destructable player = GetComponent<Destructable>();
    }
	
	void Start ()
    {
        ChooseShootWeapon.GetPistol();
    }
    
    void Update ()
    {
        if (GameController.Instance.State == GameState.Play)
        {
            ChooseShootWeapon.PickUpWeapon();
            ControlWeapon();
            HUD.Ammo = ChooseShootWeapon.CurrentWeapon.ammo;
        }
    }

    private void ControlWeapon()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            ChooseShootWeapon.UseWeapon();      
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            ChooseShootWeapon.ChooseWeapon(Input.GetAxis("Mouse ScrollWheel"));

    }

    private bool Dash()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Time.time > _nextDashTime)
            {           
                //_rigidbody.AddForce(transform.forward * _dashDistance*1000, ForceMode.Force);
                _nextDashTime = Time.time + _dashDelay;
                //TODO: Add particle system on player, change particle effect while dash
                //Transform dashTransform = 
                //    Instantiate(_dashEffect, 
                //    new Vector3(transform.position.x, transform.position.y + transform.localScale.y/3.5f, transform.position.z),
                //    Quaternion.Euler(0, transform.forward.x, 0));
                MenuAudioController.Instance.PlaySound("whoosh", true);
            }
            return true;
        } return false;
      
    }
}
