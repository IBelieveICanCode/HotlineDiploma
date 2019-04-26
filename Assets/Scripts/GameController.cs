using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public ILogicDeathDependable playerEntity;
    public IVisible playerPos;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        playerEntity = FindObjectOfType<PlayerParticleDestructable>().GetComponent<ILogicDeathDependable>();
        playerPos = FindObjectOfType<PlayerParticleDestructable>().GetComponent<IVisible>();
    }
}
