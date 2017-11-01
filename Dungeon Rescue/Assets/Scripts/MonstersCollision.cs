using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersCollision : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "dragon")
        {
            gameObject.GetComponent<Knight>().TakeDamage(1);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "fireball(Clone)")
        {
            gameObject.GetComponent<Knight>().TakeDamage(2);
        }
    }
}
