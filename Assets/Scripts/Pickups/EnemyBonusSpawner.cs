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
    [SerializeField]
    private List<EnemyBonus> enemyBonusList = new List<EnemyBonus>();

    public static void Create(Vector3 _position, GameObject _bonus)
    {
        Instantiate(_bonus, _position, Quaternion.identity);
    }

    public static void SpawnEnemyBonus(Vector3 _position)
    {
        if (Instance.enemyBonusList == null)
        {
            return;
            Debug.Log("Empty enemy bonus list");
        }
        var _bonus = CalculateSpawnProbability();
        if (_bonus != null)
        {
            Create(_position, _bonus);
        }
    }

    private static GameObject CalculateSpawnProbability()
    {
        //Get random coef
        //shuffle enemy bonus list
        //compare probability
        //If can spawn something return what to spawn if 
        float _coef = Random.Range(0f, 1f);
        //print(_coef);
        ListRandomizer.Shuffle(Instance.enemyBonusList);
        foreach(var enemyBonus in Instance.enemyBonusList)
        { 
            if(_coef >= (1f - enemyBonus.SpawnProbability))
            {
                return enemyBonus.BonusPrefab;
            }
        }
        return null;
    }
}

[System.Serializable]
public class EnemyBonus
{
    //from 0 - 1
    public string BonusName;
    [Range(0f
        ,1f)]
    public float SpawnProbability = 0.15f;
    public GameObject BonusPrefab;
    ////path to load obj from recources
    //public string ResourcesPath;
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
