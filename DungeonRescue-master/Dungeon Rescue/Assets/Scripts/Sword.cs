using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	[SerializeField]
	GameObject Knight;

	Animator mAnimator;
	bool mAttacking;
	float kAttackDuration = 0.4f;
	float mTime;
	int rayLength = 10;
	int damage = 10;

	// Use this for initialization
	void Start () {
		mAnimator = transform.parent.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && !mAttacking && !Knight.GetComponent<Knight>().getmJumping()) {
			mAttacking = true;
			mTime = 0.0f;
			Attack ();
		}

		if(mAttacking)
		{
			mTime += Time.deltaTime;
			if(mTime > kAttackDuration)
			{
				mAttacking = false;
				gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			}
		}


		UpdateAnimator();
	}

	private void UpdateAnimator()
	{
		mAnimator.SetBool ("isAttacking", mAttacking);
	}

	private void Attack() {
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		Debug.Log ("Knight Attack!");
	}

	public int getSwordDamage() {
		return damage;
	}
}
