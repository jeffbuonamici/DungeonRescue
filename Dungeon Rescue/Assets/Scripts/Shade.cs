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

		// Rules for attacking and moving
		if (mTarget != null) {
			if ((Vector2.Distance (transform.position, mTarget.transform.position) < mAggroRange)
			    && !(Vector2.Distance (transform.position, mTarget.transform.position) < mIdleRange)) {
				transform.position += (mTarget.transform.position - transform.position).normalized * Time.deltaTime * mFollowSpeed;
				isWalking = true;
			} else if (Vector2.Distance (transform.position, mTarget.transform.position) < mIdleRange && cooldownTimer <= 0) {
				isWalking = false;
				if (DetermineAttack () < 6) {
					Attack ();
				} else
					cooldownTimer = attackDelay;
			} else if(Vector2.Distance (transform.position, mTarget.transform.position) < mCloseRange) {
				System.Random rnd = new System.Random ();
				int value = rnd.Next (1, 3);
				if (value == 1) {
					transform.position -= (mTarget.transform.position - transform.position).normalized * Time.deltaTime * (mFollowSpeed / 2);
					isWalking = true;
				} else {
					isWalking = false;
					if (DetermineAttack () < 6) {
						Attack ();
					} else
						cooldownTimer = attackDelay;
				}
			} else {
				isWalking = false;
			}
		}

		if(mAttacking)
		{
			mTime += Time.deltaTime;
			if(mTime > kAttackDuration)
			{
				mAttacking = false;
				isAttacking = false;
				CircleCollider2D[] col = GetComponentsInChildren<CircleCollider2D> ();
				col [0].enabled = false;
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
		CircleCollider2D[] col = GetComponentsInChildren<CircleCollider2D> ();
		col [0].enabled = true;
		cooldownTimer = attackDelay;
		mTime = 0f;
		isAttacking = true;
		mAttacking = true;
		Debug.Log ("Attack!");
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.name == "Sword") {
			Debug.Log ("Shade Hit!");
			TakeDamage (col.gameObject.GetComponent<Sword>().getSwordDamage());
			col.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}

	void TakeDamage(int damage) {
		health -= damage;

		if (health <= 0)
			Die ();
	}

	void Die() {
		Destroy (gameObject);
	}

	private void UpdateAnimator()
	{
		mAnimator.SetBool("isWalking", isWalking);
		mAnimator.SetBool("isAttacking", isAttacking);
	}
}
