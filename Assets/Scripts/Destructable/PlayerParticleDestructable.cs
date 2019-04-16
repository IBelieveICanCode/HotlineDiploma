using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleDestructable : ParticleDestructable, IVisible
{
    Vector3 IVisible.GetCurrentPositon()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
