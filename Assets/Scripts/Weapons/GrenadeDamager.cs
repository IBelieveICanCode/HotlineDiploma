using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeDamager : Rocket
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
            Destroy(gameObject);
    }
}
