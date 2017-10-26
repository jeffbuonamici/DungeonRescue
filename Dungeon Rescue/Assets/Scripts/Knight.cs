using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

	[SerializeField]
	float mMoveSpeed;
	[SerializeField]
	float mJumpForce;

	// Animator Booleans
	bool mWalking;

	// References to other components and game objects
	Animator mAnimator;
	Rigidbody2D mRigidBody2D;

	Vector2 mFacingDirection;

	// Use this for initialization
	void Start () {
		// Get references to other components and game objects
		mAnimator = GetComponent<Animator>();
		mRigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		mWalking = false;
		// Move left
		if(Input.GetKey(KeyCode.A))
		{
			transform.Translate (-Vector2.right * mMoveSpeed * Time.deltaTime);
			FaceDirection(-Vector2.right);
			mWalking = true;
		}
		// Move right
		if(Input.GetKey(KeyCode.D))
		{
			transform.Translate (Vector2.right * mMoveSpeed * Time.deltaTime);
			FaceDirection(Vector2.right);
			mWalking = true;
		}
		UpdateAnimator();
	}

	public Vector2 GetFacingDirection()
	{
		return mFacingDirection;
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

	private void UpdateAnimator()
	{
		mAnimator.SetBool ("isWalking", mWalking);
		//mAnimator.SetBool ("isGrounded", mGrounded);
	}
}
