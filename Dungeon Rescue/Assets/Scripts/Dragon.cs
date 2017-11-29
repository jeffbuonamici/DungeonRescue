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
    int health;

    public Transform repelPoint;
    public Transform midPoint;

    [SerializeField]
    GameObject mfireBreathPrefab;

    public Animator mAnimator;

    float fireBreathTimer=0;

    public float attackDelay;
	float cooldownTimer = 0f;

    bool backTrace;
	bool isWalking;
	bool isFireAttack;
    int damageDefault = 1;

    //GameObject fireBreath;
    // Use this for initialization
    void Start () {
        isFireAttack = false;
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;

        //"Max chase"
        if (Vector2.Distance(transform.position, repelPoint.transform.position) < 5)
        {
            isFireAttack = false;
            Debug.Log("a");
            transform.position -= (repelPoint.transform.position - transform.position).normalized * Time.deltaTime;
            isWalking = false;
            backTrace = true;
        }
        //Return to spawn
        else if (Vector2.Distance(transform.position, midPoint.transform.position) > 5 && backTrace)
        {
            Debug.Log("b");
            transform.position += (midPoint.transform.position - transform.position).normalized * Time.deltaTime;
            isWalking = true;
            if (Vector2.Distance(transform.position, midPoint.transform.position) <= 5){
                backTrace = false;
                Debug.Log("bb");
            }
        }


        else if(mTarget != null)
		{
            Debug.Log("c");
            //melee range
            if (Vector2.Distance(transform.position, mTarget.transform.position) < mCloseRange)
                //add cooldown to claw
            {
                Debug.Log("melee range");
                //claw attack?
                if (DetermineAttack() < 11)
                {
                    FireAttack();
                }
                else
                {
                    cooldownTimer = attackDelay;
                }
                isWalking = false;
                // transform.position -= (mTarget.transform.position - transform.position).normalized * Time.deltaTime;
            }
            //fire range
            else if (Vector2.Distance(transform.position, mTarget.transform.position) < mIdleRange
                && cooldownTimer <= 0)
            {
                Debug.Log("fire range");
                isWalking = false;
                if (DetermineAttack() < 11)
                {
                    FireAttack();
                }
                else
                {
                    cooldownTimer = attackDelay;
                }
            }
            //in vision and out of attack range
            else if ((Vector2.Distance(transform.position, mTarget.transform.position) < mAggroRange)
                    && (Vector2.Distance(transform.position, mTarget.transform.position) > mIdleRange)
                     && !isFireAttack)
                {
                    Debug.Log("vision range");
                    transform.position += (mTarget.transform.position - transform.position).normalized * Time.deltaTime;
                    isWalking = true;
                }
            //out of sight
            else if (Vector2.Distance(transform.position, mTarget.transform.position) > mAggroRange
                && !isFireAttack)
            {
                isWalking = false;
            }
		}

        UpdateAnimator();
	}

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isWalking", isWalking);
        mAnimator.SetBool("isFireAttack", isFireAttack);
    }


    int DetermineAttack() {
		System.Random rnd = new System.Random ();
		int value = rnd.Next (1, 10);
		return value;
	}

	void FireAttack() {
		cooldownTimer = attackDelay;
		isFireAttack = true;
		Debug.Log ("Attack!");
        Invoke("stopFire", 2.0f);
        isWalking = false;
    }

    void stopFire()
    {
        isFireAttack = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Sword")
        {
            Debug.Log("Dragon Hit!");
            TakeDamage(col.gameObject.GetComponent<Sword>().getSwordDamage());
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public int DealDamage()
    {
        if (isFireAttack)
        {
            Debug.Log("2 damage");
            return 2;
        }
        else
        {
            return damageDefault;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
