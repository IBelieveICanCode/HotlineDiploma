using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //public Transform Player;
    //private Vector3
    //    _offset,
    //    _cameraMove;

    //void Start()
    //{
    //    _offset = transform.position - Player.position;
    //}
    //void Update()
    //{
    //    CameraInGamePosition();
    //}

    //private void CameraInGamePosition()
    //{
    //    _cameraMove = Player.position + _offset;
    //    transform.position = _cameraMove;
    //}

    private void Update()
    {
        transform.eulerAngles = new Vector3(90f, 0f);
    }
}
