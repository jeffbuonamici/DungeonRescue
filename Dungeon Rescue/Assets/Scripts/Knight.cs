using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

	[SerializeField]
	float mMoveSpeed;
	[SerializeField]
	float mJumpForce;
    [SerializeField]
    LayerMask mWhatIsGround;
    float kGroundCheckRadius = 0.1f;

    [SerializeField]
    HeartBar life;

    // Animator Booleans
    bool mWalking;

	// References to other components and game objects
	Animator mAnimator;
	Rigidbody2D mRigidBody2D;
    public List<GroundCheck> mGroundCheckList;

    Vector2 mFacingDirection;

    // Invincibility timer
    float kInvincibilityDuration = 1.0f;
    float mInvincibleTimer;
    bool mInvincible;
    float kDamagePushForce = 2.5f;

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
        bool grounded = CheckGrounded();

        if (grounded && Input.GetKey(KeyCode.W) && GetComponent<Rigidbody2D>().velocity.y < 0.1)
        {
            //Debug.Log("My jump velocity when I jump is: " + mJumpForce);
            mRigidBody2D.AddForce(Vector2.up * mJumpForce, ForceMode2D.Impulse);
        }
        
        UpdateAnimator();

        if (mInvincible)
        {
            mInvincibleTimer += Time.deltaTime;
            if (mInvincibleTimer >= kInvincibilityDuration)
            {
                mInvincible = false;
                mInvincibleTimer = 0.0f;
            }
        }
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

    private bool CheckGrounded()
    {
        foreach (GroundCheck g in mGroundCheckList)
        {
            if (g.CheckGrounded(kGroundCheckRadius, mWhatIsGround, gameObject))
            {
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(int dmg)
    {
        if (!mInvincible)
        {
            Vector2 forceDirection = new Vector2(-mFacingDirection.x, 1.0f) * kDamagePushForce;
            mRigidBody2D.velocity = Vector2.zero;
            mRigidBody2D.AddForce(forceDirection, ForceMode2D.Impulse);
            mInvincible = true;
            life.loseHeart(dmg);
        }
    }

    public void HealHP(int health)
    {
        life.gainHeart(health);
    }
}
