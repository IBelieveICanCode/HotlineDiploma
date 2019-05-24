using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicControl : MonoBehaviour
{
    [SerializeField]
    protected CharacterController characterController;
    [SerializeField]
    protected float _speed = 10f;

    protected virtual void FixedUpdate()
    {
        //LookAtTarget();
        MovePlayer();

    }

    protected virtual void MovePlayer()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * _speed;
        characterController.Move(moveVelocity * Time.deltaTime);
    }


    protected virtual void LookAtTarget()
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
