using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    private void Update()
    {
        transform.eulerAngles = new Vector3(45f, 30f);
    }
}
