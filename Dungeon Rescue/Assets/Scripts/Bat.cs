using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.name == "Knight") {
			Destroy (gameObject);
			col.gameObject.GetComponent<Knight>().TakeDamage(1);
		}
	}
}
