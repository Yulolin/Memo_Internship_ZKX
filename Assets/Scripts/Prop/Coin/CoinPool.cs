
using System;
using UnityEngine;
using UnityEngine.Pool;

public class CoinPool :MonoBehaviour
{
    public static CoinPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject goldCoinPrefab;
    private ObjectPool<GameObject> goldCoinPool;
    public GameObject silverCoinPrefab;
    private ObjectPool<GameObject> silverCoinPool;

    private void Start()
    {
        goldCoinPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(goldCoinPrefab),
            actionOnGet: obj => obj.SetActive(true),
            actionOnRelease: obj => obj.SetActive(false),
            actionOnDestroy: obj => Destroy(obj),
            maxSize: 100
        );
        silverCoinPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(silverCoinPrefab),
            actionOnGet: obj => obj.SetActive(true),
            actionOnRelease: obj => obj.SetActive(false),
            actionOnDestroy: obj => Destroy(obj),
            maxSize: 100
        );
    }

    public GameObject GetRandomCoin()
    {
        int random =  UnityEngine.Random.Range(0, 3);
        GameObject coin = random==2?goldCoinPool.Get():silverCoinPool.Get();
        return coin;
    }

    public GameObject GetGoldCoin()
    {
        return goldCoinPool.Get();
    }

    public void ReturnGoldCoin(GameObject goldCoin)
    {
        goldCoinPool.Release(goldCoin);
    }

    public GameObject GetSilverCoin()
    {
        return silverCoinPool.Get();
    }

    public void ReturnSilverCoin(GameObject silverCoin)
    {
        silverCoinPool.Release(silverCoin);
    }
    
    private void OnDestroy()
    {
        goldCoinPool.Clear();
        silverCoinPool.Clear();
    }
}
