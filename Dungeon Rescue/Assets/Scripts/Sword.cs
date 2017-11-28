using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	Animator mAnimator;
	bool mAttacking;

	float kAttackDuration = 0.25f;
	float mTime;

	// Use this for initialization
	void Start () {
		mAnimator = transform.parent.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.Space))
		{
			mAttacking = true;
			mTime = 0.0f;
		}

		if(mAttacking)
		{
			mTime += Time.deltaTime;
			if(mTime > kAttackDuration)
			{
				mAttacking = false;
			}
		}

		UpdateAnimator();
	}

	private void UpdateAnimator()
	{
		mAnimator.SetBool ("isAttacking", mAttacking);
	}
}
