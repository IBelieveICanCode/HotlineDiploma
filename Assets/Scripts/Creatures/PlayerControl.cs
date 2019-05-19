using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (CharacterController))]
public class PlayerControl: ChooseShootWeapon
{
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private Transform _dashEffect;
    private float _nextDashTime = 0;
    [SerializeField]
    private float _dashDelay;
    [SerializeField]
    private float _dashDistance = 5f;
    [SerializeField]
    private float _speed = 10f;
    
    void Awake ()
    {
        Destructable player = GetComponent<Destructable>();
    }
	
	void Start ()
    {
        GetPistol();
    }
    
    void Update ()
    {
        if (GameController.Instance.State == GameState.Play)
        {
            LookAtTarget();
            PickUpWeapon();
            ControlCharacter();
            HUD.Ammo = CurrentWeapon.ammo;
        }
    }

    private void ControlCharacter()
    {
        if (Input.GetKey(KeyCode.Mouse0))   
            UseWeapon();      
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            ChooseWeapon(Input.GetAxis("Mouse ScrollWheel"));


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


    void FixedUpdate()
    {
        //if (!Dash())
            ApplyMovingForce();
      
    }
    
    private void ApplyMovingForce()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * _speed;
        characterController.Move(moveVelocity * Time.deltaTime); 
    }


    private void LookAtTarget()
    {

        Plane plane = new Plane(Vector3.up, transform.position);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);     
        if (plane.Raycast(ray, out distance))
        {
            Vector3 position = ray.GetPoint(distance); 
            //position.y = transform.position.y;
            transform.LookAt(position);
        }
    }
}
