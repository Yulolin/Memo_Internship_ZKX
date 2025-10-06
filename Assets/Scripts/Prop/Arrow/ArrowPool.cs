using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject arrowPrefab; // 箭矢预制体
    private ObjectPool<GameObject> arrowPool; // 箭矢对象池

    void Start()
    {
        // 初始化对象池
        arrowPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(arrowPrefab),         // 创建箭矢的函数
            actionOnGet: obj => obj.SetActive(true),            // 获取箭矢时激活它
            actionOnRelease: obj => obj.SetActive(false),       // 释放箭矢时禁用它
            actionOnDestroy: obj => Destroy(obj),               // 销毁箭矢时销毁它
            maxSize: 50                                           // 最大池容量
        );
    }

    // 发射箭矢
    public GameObject GetArrow(Transform createPos)
    {
        GameObject arrow = arrowPool.Get();
        arrow.transform.position = createPos.position;
        arrow.transform.rotation = createPos.rotation;
        return arrow;  // 从池中获取一个箭矢
    }

    // 回收箭矢
    public void ReturnArrow(GameObject arrow)
    {
        if (arrow.activeSelf == false)
        {
            return;
        }
        arrowPool.Release(arrow);  // 将箭矢归还给池中
    }

    // 清理对象池
    void OnDestroy()
    {
        arrowPool.Clear();
    }
}