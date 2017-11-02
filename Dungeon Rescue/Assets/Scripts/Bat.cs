﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {
    [SerializeField]
    GameObject mPotionPrefab;

    GameObject potion;

    void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.name == "Knight") {
			Destroy (gameObject);
			col.gameObject.GetComponent<Knight>().TakeDamage(1);
            potion = Instantiate(mPotionPrefab, new Vector3(transform.position.x - 0.1f, transform.position.y + 0.5f), Quaternion.identity);
            potion.name = "potion";
        }
	}
}
