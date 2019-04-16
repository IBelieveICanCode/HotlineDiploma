﻿    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class Spawner : MonoBehaviour
        {
    [Header("Types of Enemies")]
    [SerializeField]
    private Enemy[] typesOfEnemies; // types of enemies in general

    public Wave[] waves;
    Wave currentWave;
    private int currentWaveNumber;
    public int CurrentWaveNumber { get => currentWaveNumber; set => currentWaveNumber = value; }

    [SerializeField]
    ILogicDeathDependable playerEntity;
    [SerializeField]
    IVisible playerPos;
    private Vector3 playerT;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    MapGenerator map;

    float timeBetweenCampingChecks = 2;
    float campThresholdDistance = 1.5f;
    float nextCampCheckTime;
    Vector3 campPositionOld;
    bool isCamping;

    bool isDisabled;

    public event System.Action<int> OnNewWave;

    void Start()
    {
        playerPos = FindObjectOfType<PlayerParticleDestructable>().GetComponent<IVisible>();
        playerEntity = FindObjectOfType<PlayerParticleDestructable>().GetComponent<ILogicDeathDependable>();
        playerT = playerPos.GetCurrentPositon();
        nextCampCheckTime = timeBetweenCampingChecks + Time.time;
        campPositionOld = playerT;
        playerEntity.DeathEvent += OnPlayerDeath;

        map = FindObjectOfType<MapGenerator>();
        StartCoroutine(NextWave(true));
    }

    void Update()
    {
        if (!isDisabled)
        {
            if (Time.time > nextCampCheckTime)
            {
                nextCampCheckTime = Time.time + timeBetweenCampingChecks;

                isCamping = (Vector3.Distance(playerT, campPositionOld) < campThresholdDistance);
                campPositionOld = playerT;
            }
            if (currentWave != null)
                if ((enemiesRemainingToSpawn > 0 || currentWave.infinite) && Time.time > nextSpawnTime)
                {
                    enemiesRemainingToSpawn--;
                    nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
                    StartCoroutine(SpawnEnemy());
                }
        }
    }

    IEnumerator SpawnEnemy()
    {
        float spawnDelay = 1;
        float tileFlashSpeed = 4;

        Transform spawnTile = map.GetRandomOpenTile();
        if (isCamping)
        {
            spawnTile = map.GetTileFromPosition(playerT);
        }
        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColour = tileMat.color;
        Color flashColour = Color.black;
        float spawnTimer = 0;

        while (spawnTimer < spawnDelay)
        {

            tileMat.color = Color.Lerp(initialColour, flashColour, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));

            spawnTimer += Time.deltaTime;
            yield return null;
        }

        Enemy newEnemy = Instantiate(typesOfEnemies[Random.Range(0, typesOfEnemies.Length)], spawnTile.position + Vector3.up, Quaternion.identity);
        newEnemy.target = GameObject.FindGameObjectWithTag("Player").transform;
        newEnemy.GetComponent<ILogicDeathDependable>().DeathEvent += OnEnemyDeath;
    }

    void OnPlayerDeath()
    {
        isDisabled = true;
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive == 0)
        {
        StartCoroutine(NextWave(false));
            //NextWave();

        //FillEnemyStack();
        }
    }

    void ResetPlayerPosition()
        {
            playerPos.SetPosition(map.GetTileFromPosition(Vector3.zero).position);
        }

    //void NextWave()
    IEnumerator NextWave(bool firstSpawn)
        {
        if (firstSpawn)
            yield return null;
        else
            yield return new WaitForSeconds(3f);
            CurrentWaveNumber++;
            
            HUD.CurrentWave = CurrentWaveNumber;
            if (CurrentWaveNumber - 1 < waves.Length)
            {
                currentWave = waves[CurrentWaveNumber - 1];

                enemiesRemainingToSpawn = currentWave.enemyCount;
                enemiesRemainingAlive = enemiesRemainingToSpawn;

            if (OnNewWave != null)
            {
                OnNewWave(CurrentWaveNumber);
            }
            ResetPlayerPosition();
            }
        }

    /*
    void FillEnemyStack()
    {
        for (int i = 0; i < typesOfEnemies.Length; i++)
        {
            print(enemies.Count.ToString());
            enemies.Add(typesOfEnemies[Random.Range(0, typesOfEnemies.Length)]);
            
        }
    }
    */

        [System.Serializable]
        public class Wave
        {
            public bool infinite;
            public int enemyCount;
            public float timeBetweenSpawns;

        
        }

    }

  

