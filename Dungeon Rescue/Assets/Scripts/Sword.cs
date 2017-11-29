using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	[SerializeField]
	GameObject Knight;

	Animator mAnimator;
	bool mAttacking;
    bool mIce;
	float kAttackDuration = 0.4f;
	float mTime;
	int rayLength = 10;
	int damage = 10;

	// Use this for initialization
	void Start () {
		mAnimator = transform.parent.GetComponent<Animator>();
        mIce = false;
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)){
            mIce = !mIce;
        }

        if (Input.GetKeyDown (KeyCode.Space) && !mAttacking && !Knight.GetComponent<Knight>().getmJumping()) {
			mAttacking = true;
			mTime = 0.0f;
			Attack();
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
        mAnimator.SetBool("isIce", mIce);
    }

	private void Attack() {
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		Debug.Log ("Knight Attack!");
	}

	public int getSwordDamage() {
        if (mIce)
        {
            damage = 20;
        }
        else
        {
            damage = 10;
        }
		return damage;
	}
}
