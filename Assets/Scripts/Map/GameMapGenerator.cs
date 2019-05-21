using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapGenerator : MapGenerator
{
    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<Spawner>().OnNewWave += OnNewWave;
    }

    protected void OnNewWave(int waveNumber)
    {
        mapIndex = waveNumber - 1;
        GenerateMap();
    }
}
