using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBonusSpawner : MonoBehaviour
{
    #region Singleton
    public static EnemyBonusSpawner Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion
    private List<EnemyBonus> enemyBonusList = new List<EnemyBonus>();

    public static void Create(Vector3 _position, string _path)
    {
        Instantiate(Resources.Load(_path), _position, Quaternion.identity);
    }

    public static void SpawnEnemyBonus(Vector3 _position)
    {
        if (Instance.enemyBonusList == null)
        {
            return;
            Debug.Log("Empty enemy bonus list");
        }
        string _resourcesPath = CalculateSpawnProbability();
        if (!string.IsNullOrEmpty(_resourcesPath))
        {
            Create(_position, _resourcesPath);
        }
    }

    private static string CalculateSpawnProbability()
    {
        //Get random coef
        //shuffle enemy bonus list
        //compare probability
        //If can spawn something return what to spawn if 
        float _coef = Random.Range(0f, 1f);
        ListRandomizer.Shuffle(Instance.enemyBonusList);
        foreach(var enemyBonus in Instance.enemyBonusList)
        { 
            if(_coef >= (1f - enemyBonus.SpawnProbability))
            {
                return enemyBonus.ResourcesPath;
            }
        }
        return "";
    }
}

[System.Serializable]
public class EnemyBonus
{
    //from 0 - 1
    public string BonusName;
    public float SpawnProbability = 0.15f;
    public GameObject BonusPrefab;
    //path to load obj from recources
    public string ResourcesPath;
}

public static class ListRandomizer
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
