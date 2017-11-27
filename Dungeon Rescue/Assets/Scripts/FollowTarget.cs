using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

	[SerializeField]
	Transform mTarget;
	[SerializeField]
	float mFollowSpeed;
	[SerializeField]
	float mFollowRange;

	float mArriveThreshold = 0.05f;
	Vector2 mFacingDirection;

	void Update ()
	{
		if(mTarget != null)
		{
			if (Vector2.Distance (transform.position, mTarget.transform.position) < mFollowRange) {
				transform.position += (mTarget.transform.position - transform.position).normalized * Time.deltaTime;

				if (mTarget.position.x < transform.position.x) {
					FaceDirection(-Vector2.right);
				} else if(mTarget.position.x >= transform.position.x){
					FaceDirection(Vector2.right);
				}
			}
		}
	}

	public void SetTarget(Transform target)
	{
		mTarget = target;
	}

	bool changed(float a,float b){
		if((int)a == (int)b){
			return true;
		}else{
			return false;
		}
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
}
