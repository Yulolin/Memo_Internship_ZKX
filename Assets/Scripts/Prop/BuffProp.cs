using System;
using UnityEngine;

public class BuffProp : MonoBehaviour
{
    public BuffType buffType;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().ChangeBuff(buffType);
            Destroy(gameObject);
        }
    }
}
