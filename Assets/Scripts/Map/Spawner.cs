    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class Spawner : MonoBehaviour
        {
    [Header("Types of Enemies")]
    [SerializeField]
    private Enemy[] typesOfEnemies; // types of enemies in general
    private List<Enemy> enemies; // from this list enemies will spawn
    


    public Wave[] waves;
        

    [SerializeField]
    Destructable playerEntity;
    Transform playerT;

    Wave currentWave;
    public int currentWaveNumber;

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
    

    public int CurrentWaveNumber { get => currentWaveNumber; set => currentWaveNumber = value; }

    public event System.Action<int> OnNewWave;

    void Start()
    {
            playerT = playerEntity.transform;

            nextCampCheckTime = timeBetweenCampingChecks + Time.time;
            campPositionOld = playerT.position;
            playerEntity.OnDeath += OnPlayerDeath;

            map = FindObjectOfType<MapGenerator>();
        StartCoroutine(NextWave(true));
            //NextWave();
            //FillEnemyStack();
    }

    void Update()
    {
        if (!isDisabled)
        {
            if (Time.time > nextCampCheckTime)
            {
                nextCampCheckTime = Time.time + timeBetweenCampingChecks;

                isCamping = (Vector3.Distance(playerT.position, campPositionOld) < campThresholdDistance);
                campPositionOld = playerT.position;
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
            spawnTile = map.GetTileFromPosition(playerT.position);
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
        newEnemy.GetComponent<Destructable>().OnDeath += OnEnemyDeath;
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
            playerT.position = map.GetTileFromPosition(Vector3.zero).position;
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

  

