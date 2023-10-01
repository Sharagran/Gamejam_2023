using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnBoulderCollision : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.name == "Boulder")
        {
            Destroy(gameObject);
        }
    }


    // is trigger is checked
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Boulder")
        {
            Destroy(gameObject);
        }
    }
}
