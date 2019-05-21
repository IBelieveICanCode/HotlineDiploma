using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMap : MonoBehaviour
{
    [SerializeField]
    private Transform border;

    public MapExtended [] maps;
    //public int mapIndex;

    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Transform mapFloor;
    //public Transform navmeshFloor;
   // public Transform navmeshMaskPrefab;
    public Vector2 maxMapSize;

    [Range(0, 1)]
    public float outlinePercent;
    public float tileSize;
    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;
    Queue<Coord> shuffledOpenTileCoords;
    Transform[,] tileMap;

    MapExtended currentMap;

    private Vector3 initPoint = Vector3.zero;
    private Vector3 lastInitPoint = Vector3.zero;

    protected virtual void Awake()
    {
        GenerateMap();
    }


    public void GenerateMap()
    {
        for (int k = 0; k < maps.Length; k++)
        {
            currentMap = maps[k];
            for (int i = 0; i < currentMap.PosibleDirections.Length; i++)
            {
                print( string.Format("Current map open direction {0}, current map flag {1}, elementNum {2}", currentMap.PosibleDirections[i].OpenPos, currentMap.PosibleDirections[i].Open,i));
            }
            if (k == 0)
            {
                initPoint = Vector3.zero;
                lastInitPoint = Vector3.zero;
            }
            else
            {
                //Can't close open direction in prev map
                //initPoint = current mapPosition + RandomOpendirection * prevMapSize *(randomOpendirectionComponent) * tileSize ;
                List<PossibleDirection> possiblesDir = new List<PossibleDirection>();
                for(int i = 0; i< maps[k-1].PosibleDirections.Length;i++)
                {
                    if (maps[k - 1].PosibleDirections[i].Open)
                    {
                        possiblesDir.Add(currentMap.PosibleDirections[i]);
                    }
                }
                if (possiblesDir.Count > 0) {
                    int randElement = Random.Range(0, possiblesDir.Count);
                    Vector2 mapShiftDirection = possiblesDir[randElement].OpenPos;

                    for (int i = 0; i < currentMap.PosibleDirections.Length; i++)
                    {
                        if (currentMap.PosibleDirections[i].OpenPos == -mapShiftDirection)
                        {
                            currentMap.PosibleDirections[i].Open = false;
                        }
                    }
                    Vector2 prevMapSize = new Vector2(maps[k - 1].mapSize.x, maps[k - 1].mapSize.y) /2;
                    Vector2 adderLast =  mapShiftDirection * prevMapSize;
                    Vector2 currentMapSize = new Vector2(currentMap.mapSize.x, currentMap.mapSize.y) / 2;
                    Vector2 adderCurrent = mapShiftDirection * currentMapSize;
                    initPoint = lastInitPoint + new Vector3(adderLast.x,0f,adderLast.y) *tileSize + new Vector3(adderCurrent.x, 0f, adderCurrent.y) * tileSize;
                    lastInitPoint = initPoint;
                }
            }

            tileMap = new Transform[currentMap.mapSize.x, currentMap.mapSize.y];
            currentMap.seed = Random.Range(0, int.MaxValue);
            System.Random prng = new System.Random(currentMap.seed);

            // Generating coords
            allTileCoords = new List<Coord>();
            for (int x = 0; x < currentMap.mapSize.x; x++)
            {
                for (int y = 0; y < currentMap.mapSize.y; y++)
                {
                    allTileCoords.Add(new Coord(x, y));
                       
                }
            }
            shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), currentMap.seed));

            string holderName = "Generated Map" + k.ToString();

            Transform mapHolder = new GameObject(holderName).transform;
            mapHolder.parent = transform;

            //Spawning borders
            float _thickness = 0.5f;
            Vector2 _mapBorders = new Vector2(currentMap.mapSize.x * tileSize, currentMap.mapSize.y * tileSize);

            Vector3 _bPos = new Vector3(currentMap.mapSize.x * tileSize / 2 + _thickness / 2, 0f, 0f) + initPoint;
            Transform b = Instantiate(border, _bPos, Quaternion.identity) as Transform;
            b.localScale = new Vector3(_thickness, 5f, _mapBorders.y);
            b.parent = mapHolder;

            _bPos = new Vector3(-currentMap.mapSize.x * tileSize / 2 - _thickness / 2, 0f, 0f) + initPoint;
            b = Instantiate(border, _bPos, Quaternion.identity) as Transform;
            b.localScale = new Vector3(_thickness, 5f, _mapBorders.y);
            b.parent = mapHolder;

            _bPos = new Vector3(0f, 0f, currentMap.mapSize.y * tileSize / 2 + _thickness / 2) + initPoint;
            b = Instantiate(border, _bPos, Quaternion.identity) as Transform;
            b.localScale = new Vector3(_mapBorders.x, 5f, _thickness);
            b.parent = mapHolder;

            _bPos = new Vector3(0f, 0f, -currentMap.mapSize.y * tileSize / 2 - _thickness / 2) + initPoint;
            b = Instantiate(border, _bPos, Quaternion.identity) as Transform;
            b.localScale = new Vector3(_mapBorders.x, 5f, _thickness);
            b.parent = mapHolder;


            // Spawning tiles
            
            for (int x = 0; x < currentMap.mapSize.x; x++)
            {
                for (int y = 0; y < currentMap.mapSize.y; y++)
                {
                    Vector3 tilePosition = CoordToPosition(x, y) + initPoint;
                    Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                    newTile.localScale = Vector3.one * (1 - outlinePercent) * tileSize;
                    newTile.parent = mapHolder;
                    tileMap[x, y] = newTile;
                }
            }

            // Spawning obstacles
            bool[,] obstacleMap = new bool[(int)currentMap.mapSize.x, (int)currentMap.mapSize.y];

            int obstacleCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y * currentMap.obstaclePercent);
            int currentObstacleCount = 0;
            List<Coord> allOpenCoords = new List<Coord>(allTileCoords);

            for (int i = 0; i < obstacleCount; i++)
            {
                Coord randomCoord = GetRandomCoord();
                obstacleMap[randomCoord.x, randomCoord.y] = true;
                currentObstacleCount++;

                if (randomCoord != currentMap.mapCentre && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
                {
                    float obstacleHeight = Mathf.Lerp(currentMap.minObstacleHeight, currentMap.maxObstacleHeight, (float)prng.NextDouble());
                    Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y) + initPoint;

                    Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * obstacleHeight / 2, Quaternion.identity) as Transform;
                    newObstacle.parent = mapHolder;
                    newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, obstacleHeight, (1 - outlinePercent) * tileSize);

                    Renderer obstacleRenderer = newObstacle.GetComponent<Renderer>();
                    Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
                    float colourPercent = randomCoord.y / (float)currentMap.mapSize.y;
                    obstacleMaterial.color = Color.Lerp(currentMap.foregroundColour, currentMap.backgroundColour, colourPercent);
                    obstacleRenderer.sharedMaterial = obstacleMaterial;

                    allOpenCoords.Remove(randomCoord);
                }
                else
                {
                    obstacleMap[randomCoord.x, randomCoord.y] = false;
                    currentObstacleCount--;
                }
            }

            shuffledOpenTileCoords = new Queue<Coord>(Utility.ShuffleArray(allOpenCoords.ToArray(), currentMap.seed));

            //// Creating navmesh mask
            //Transform maskLeft = Instantiate(navmeshMaskPrefab, Vector3.left * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize + initPoint, Quaternion.identity) as Transform;
            //maskLeft.parent = mapHolder;
            //maskLeft.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

            //Transform maskRight = Instantiate(navmeshMaskPrefab, Vector3.right * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize + initPoint, Quaternion.identity) as Transform;
            //maskRight.parent = mapHolder;
            //maskRight.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

            //Transform maskTop = Instantiate(navmeshMaskPrefab, Vector3.forward * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize + initPoint, Quaternion.identity) as Transform;
            //maskTop.parent = mapHolder;
            //maskTop.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

            //Transform maskBottom = Instantiate(navmeshMaskPrefab, Vector3.back * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize + initPoint, Quaternion.identity) as Transform;
            //maskBottom.parent = mapHolder;
            //maskBottom.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

            //navmeshFloor.localScale = new Vector3(maxMapSize.x, maxMapSize.y) * tileSize;

            Transform localFloor = Instantiate(mapFloor, initPoint+Vector3.down*0.1f, Quaternion.identity);
            localFloor.eulerAngles = new Vector3(90f, 0f);
            localFloor.localScale = new Vector3(currentMap.mapSize.x * tileSize, currentMap.mapSize.y * tileSize, 1f);
            localFloor.parent = mapHolder;

           
        }
    }

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(currentMap.mapCentre);
        mapFlags[currentMap.mapCentre.x, currentMap.mapCentre.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y - currentObstacleCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-currentMap.mapSize.x / 2f + 0.5f + x, 0, -currentMap.mapSize.y / 2f + 0.5f + y) * tileSize ;
    }

    public Transform GetTileFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / tileSize + (currentMap.mapSize.x - 1) / 2f);
        int y = Mathf.RoundToInt(position.z / tileSize + (currentMap.mapSize.y - 1) / 2f);
        x = Mathf.Clamp(x, 0, tileMap.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, tileMap.GetLength(1) - 1);
        return tileMap[x, y];
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public Transform GetRandomOpenTile()
    {
        Coord randomCoord = shuffledOpenTileCoords.Dequeue();
        shuffledOpenTileCoords.Enqueue(randomCoord);
        return tileMap[randomCoord.x, randomCoord.y];
    }

}
[System.Serializable]
 public class MapExtended : Map
{
    public PossibleDirection[] PosibleDirections = new PossibleDirection[]
    {
        new PossibleDirection(Vector2.up,true),
        new PossibleDirection(Vector2.down, true) ,
        new PossibleDirection(Vector2.right, true) ,
        new PossibleDirection(Vector2.left, true)
    };

    public void GetRandomPossibleDirrection()
    {

    }
}

public class PossibleDirection
{
    public Vector2 OpenPos;
    public bool Open;

    public PossibleDirection(Vector3 dir, bool flag)
    {
        OpenPos = dir;
        Open = flag;
    }
}