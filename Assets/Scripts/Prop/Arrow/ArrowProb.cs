using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProb : MonoBehaviour
{
    public ArrowController arrowController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("Wall"))
        {
            arrowController.TouchWall();
        }
    }
}
