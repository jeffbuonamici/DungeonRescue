using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropDamage : MonoBehaviour
{
    public Dragon dragon;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "dragon" || col.gameObject.tag == "floor")
        {
            if (col.gameObject.name == "dragon")
            {
                Debug.Log("oof");
                dragon.GetComponent<Dragon>().TakeDamage(10);
               
            }
            Destroy(this.gameObject);
        }
    }
}