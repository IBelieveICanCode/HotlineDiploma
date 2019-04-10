﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void UpdateHeroParametersHandler(HeroUpgrades parameters);
[RequireComponent(typeof (Rigidbody))]
public class PlayerControl: ChooseShootWeapon
{
    
    public event UpdateHeroParametersHandler OnUpdateHeroParameters;

    public HeroUpgrades _hero;
    [SerializeField]
    private Transform _dashEffect;
    [SerializeField]
    private Animator _animator;

    private Rigidbody _rigidbody;   
    private float _nextDashTime = 0;
    [SerializeField]
    private float _dashDelay;
    

    void Awake ()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        Destructable player = GetComponent<Destructable>();
        player.OnDeath += YouDied;
    }
	
	void Start ()
    {
        GetPistol();

    }
    
    void Update ()
    {
        //OpenUpgrade();
        OpenMenu();
        LookAtTarget();
        
        PickUpWeapon();
        //ChooseWeapon();
        ControlCharacter();
        
        HUD.Ammo = CurrentWeapon.ammo;
        
    }

    private void ControlCharacter()
    {
        if (Input.GetKey(KeyCode.Mouse0))   
            UseWeapon();      
        if (Input.GetKeyUp(KeyCode.G))
            UseGrenade();
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            ChooseWeapon(Input.GetAxis("Mouse ScrollWheel"));


    }

    private bool Dash()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Time.time > _nextDashTime)
            {
                Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));              
                _rigidbody.AddForce(moveInput.normalized * _hero.DashDistance, ForceMode.Impulse);
                Transform dashTransform = 
                    Instantiate(_dashEffect, 
                    new Vector3(transform.position.x, transform.position.y + transform.localScale.y/3.5f, transform.position.z),
                    Quaternion.Euler(0, moveInput.x * 90, 0));
                MenuAudioController.Instance.PlaySound("whoosh", true);
                //_rigidbody.mass = 1f;
                _nextDashTime = Time.time + _dashDelay;
            }
            return true;
        } return false;
      
    }


    void FixedUpdate()
    {
        if (!Dash())
            ApplyMovingForce();
        CheckY();
    }

    void CheckY()
    {
        if (transform.position.y > 0.1)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
    
    private void ApplyMovingForce()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //GetComponent<Animations>().SetVariables("hSpeed", Input.GetAxisRaw("Horizontal"));
        //GetComponent<Animations>().SetVariables("vSpeed", Input.GetAxis("Vertical"));
        _animator.SetFloat("velocity", moveInput.magnitude);
        Vector3 moveVelocity = moveInput.normalized * _hero.Speed;
        _rigidbody.MovePosition(_rigidbody.position + moveVelocity * Time.fixedDeltaTime);
        
    }


    private void LookAtTarget()
    {

        Plane plane = new Plane(Vector3.up, transform.position);

        //RaycastHit hit; 

        float distance;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (plane.Raycast(ray, out distance))
        {
            Vector3 position = ray.GetPoint(distance); 
            //position.y = transform.position.y;
            transform.LookAt(position);
        }
    }

    private void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HUD.Instance.ShowWindow(HUD.Instance.MainMenu);
        }
    }
    /*
    private void OpenUpgrade()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            HUD.Instance.ShowWindow(GameObject.FindGameObjectWithTag("UpgradeWindow"));
        }
    }
    */


    private void YouDied()
    {
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
