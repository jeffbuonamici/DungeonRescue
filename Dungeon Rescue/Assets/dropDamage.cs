using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropDamage : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "dragon" || col.gameObject.name == "Platform")
        {
            Destroy(this.gameObject);
        }
    }
}