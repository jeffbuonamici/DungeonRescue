using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersCollision : MonoBehaviour
{

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name == "dragon")
		{
			/*gameObject.GetComponent<Knight>().TakeDamage(1);
            if (gameObject.GetComponent<Knight>().mInvincible)
            {
                gameObject.GetComponent<Knight>().TakeDamage(0);
            }*/
		}

		if (col.gameObject.name == "potion")
		{
			gameObject.GetComponent<Knight>().HealHP(2);
			Destroy(col.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "ShadeSword")
		{
			col.enabled = false;
			gameObject.GetComponent<Knight>().TakeDamage(1);
			Debug.Log ("Hit by Shade");
		}
	}
}