using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    private void OnDestroy()
    {
        Destroy(Instantiate<Object>(explosion, transform.position, Quaternion.identity), 0.5f);
        
    }
}
