using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    private void OnDestroy()
    {
        GameObject bulletSpark = Instantiate(explosion, transform.position, Quaternion.identity);       
    }
}
