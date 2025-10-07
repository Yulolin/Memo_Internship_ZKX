using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundController : MonoBehaviour
{
    // 大的碰撞体，用于检测区域
    private PolygonCollider2D  areaCollider;

    void Start()
    {
        areaCollider = GetComponent<PolygonCollider2D >();  // 获取当前物体上的 BoxCollider2D
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")||other.CompareTag("Prop")||other.CompareTag("Enemy"))
        {
            // 获取当前位置
            Vector3 pos = other.transform.position;

            // 判断玩家离开的位置并传送到对应的对面位置
            if (pos.x < areaCollider.bounds.min.x)  // 离开左边界
            {
                other.transform.position = new Vector3(areaCollider.bounds.max.x, pos.y, pos.z);
            }
            else if (pos.x > areaCollider.bounds.max.x)  // 离开右边界
            {
                other.transform.position = new Vector3(areaCollider.bounds.min.x, pos.y, pos.z);
            }
            else if (pos.y > areaCollider.bounds.max.y)  // 离开上边界
            {
                other.transform.position = new Vector3(pos.x, areaCollider.bounds.min.y, pos.z);
            }
            else if (pos.y < areaCollider.bounds.min.y)  // 离开下边界
            {
                other.transform.position = new Vector3(pos.x, areaCollider.bounds.max.y, pos.z);
            }
        }
    }
}
