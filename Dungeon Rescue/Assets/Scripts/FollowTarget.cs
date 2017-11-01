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

	void Update ()
	{
		if(mTarget != null)
		{
			// TODO: Make the enemy follow the target "mTarget"
			//       only if the target is close enough (distance smaller than "mFollowRange")
			if (Vector2.Distance (transform.position, mTarget.transform.position) < mFollowRange)
				transform.position += (mTarget.transform.position - transform.position).normalized * Time.deltaTime;
		}
	}

	public void SetTarget(Transform target)
	{
		mTarget = target;
	}
}
