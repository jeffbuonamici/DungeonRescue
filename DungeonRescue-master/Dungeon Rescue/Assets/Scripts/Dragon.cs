using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {

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
    GameObject mfireBreathPrefab;

    public Animator mAnimator;

    float fireBreathTimer=0;

    public float attackDelay;
	float cooldownTimer = 0f;

	bool isWalking;
	bool isAttacking;

    GameObject fireBreath;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;
		if(mTarget != null)
		{
			if ((Vector2.Distance (transform.position, mTarget.transform.position) < mAggroRange) 
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
				if (DetermineAttack() < 4)
					Attack ();
				else 
					cooldownTimer = attackDelay;
			}
            else if (Vector2.Distance(transform.position, mTarget.transform.position) > mAggroRange)
            {
                isWalking = false;
            }
		}

        if (fireBreath != null)
        {
            fireBreathTimer += Time.deltaTime;
            fireBreath.transform.position = new Vector3(transform.position.x - 4, transform.position.y + 0.6f);
            if (fireBreathTimer > 1)
            {
                Destroy(fireBreath);
                fireBreathTimer = 0;
            }
        }

        UpdateAnimator();

        //isWalking = false;
        isAttacking = false;
	}

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isWalking", isWalking);
    }


    int DetermineAttack() {
		System.Random rnd = new System.Random ();
		int value = rnd.Next (1, 10);
		return value;
	}

	void Attack() {
		cooldownTimer = attackDelay;
		isAttacking = true;
		Debug.Log ("Attack!");
        fireBreath = Instantiate(mfireBreathPrefab, new Vector3(transform.position.x-4, transform.position.y+0.6f), Quaternion.identity);
       
    }
}
