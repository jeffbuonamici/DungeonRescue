using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : MonoBehaviour {

	[SerializeField]
	Transform mTarget;
	[SerializeField]
	float mFollowSpeed;
	[SerializeField]
	float mAggroRange;
	[SerializeField]
	float mIdleRange;
	[SerializeField]
	float mCloseRange;
	[SerializeField]
	int health;
	[SerializeField]
	Animator mAnimator;
	[SerializeField]
	float attackDelay;

	float mTime = 0f;
	float kAttackDuration = 0.35f;
	float cooldownTimer = 0f;
	Vector2 mFacingDirection;
	bool isWalking;
	bool isAttacking;
	bool mAttacking;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;
		if (mTarget != null) {
			/*if ((Vector2.Distance (transform.position, mTarget.transform.position) < mAggroRange)
			    && !(Vector2.Distance (transform.position, mTarget.transform.position) < mIdleRange)) {
				transform.position += (mTarget.transform.position - transform.position).normalized * Time.deltaTime;
				isWalking = true;
			}
			if (Vector2.Distance (transform.position, mTarget.transform.position) < mCloseRange) {
				transform.position -= (mTarget.transform.position - transform.position).normalized * Time.deltaTime;
				isWalking = true;
			}
			if ((Vector2.Distance (transform.position, mTarget.transform.position) < mAggroRange)
			    && cooldownTimer <= 0) {
				if (DetermineAttack () < 4) {
					isWalking = false;
					Attack ();
				}
				else
					cooldownTimer = attackDelay;
			} else if (Vector2.Distance (transform.position, mTarget.transform.position) > mAggroRange) {
				isWalking = false;
			}*/

			if ((Vector2.Distance (transform.position, mTarget.transform.position) < mAggroRange)
				&& !(Vector2.Distance (transform.position, mTarget.transform.position) < mIdleRange)) {
				transform.position += (mTarget.transform.position - transform.position).normalized * Time.deltaTime * mFollowSpeed;
				isWalking = true;
			}

			if (Vector2.Distance (transform.position, mTarget.transform.position) < mIdleRange && cooldownTimer <= 0) {
				//transform.position -= (mTarget.transform.position - transform.position).normalized * Time.deltaTime;
				isWalking = false;
				if (DetermineAttack () < 6) {
					Attack ();
				}
				else
					cooldownTimer = attackDelay;
			}


		}

		if(mAttacking)
		{
			mTime += Time.deltaTime;
			if(mTime > kAttackDuration)
			{
				mAttacking = false;
				isAttacking = false;
				//gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			}
		}

		if(mTarget != null)
		{
			if (mTarget.position.x < transform.position.x) {
				FaceDirection(-Vector2.right);
			} else if (mTarget.position.x >= transform.position.x){
				FaceDirection(Vector2.right);
			}
		}

		UpdateAnimator();

	}

	public void SetTarget(Transform target)
	{
		mTarget = target;
	}

	private void FaceDirection(Vector2 direction)
	{
		mFacingDirection = direction;
		if(direction == Vector2.right)
		{
			Vector3 newScale = new Vector3(Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
		else
		{
			Vector3 newScale = new Vector3(-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
	}

	public Vector2 GetFacingDirection()
	{
		return mFacingDirection;
	}

	int DetermineAttack() {
		System.Random rnd = new System.Random ();
		int value = rnd.Next (1, 10);
		return value;
	}

	void Attack() {
		cooldownTimer = attackDelay;
		mTime = 0f;
		isAttacking = true;
		mAttacking = true;
		Debug.Log ("Attack!");
	}

	private void UpdateAnimator()
	{
		mAnimator.SetBool("isWalking", isWalking);
		mAnimator.SetBool("isAttacking", isAttacking);
	}
}
