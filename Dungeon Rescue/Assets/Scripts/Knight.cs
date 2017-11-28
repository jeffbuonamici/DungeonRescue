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
    float kGroundCheckRadius = 0.05f;

    [SerializeField]
    HeartBar life;

    // Animator Booleans
    bool mWalking;
    bool mJumping;

    // References to other components and game objects
    Animator mAnimator;
	Rigidbody2D mRigidBody2D;
    public List<GroundCheck> mGroundCheckList;

    Vector2 mFacingDirection;

    // Invincibility timer
    float kInvincibilityDuration = 1.0f;
    float mInvincibleTimer;
    public bool mInvincible;

    float jumpCooldDown = 1.2f;
    float mJumpTimer;
    public bool mJumpCD;

    // Stun timer
    float kStunDuration = 1.0f;
    float mStunTimer;
    bool mStun = false;

    bool grounded = true;

    float kDamagePushForce = 2.5f;

    // Use this for initialization
    void Start () {
		// Get references to other components and game objects
		mAnimator = GetComponent<Animator>();
		mRigidBody2D = GetComponent<Rigidbody2D>();
	}

    void endJump()
    {
        if (grounded)
        {
            Debug.Log("a");
            mJumping = false;
        }
    }

    // Update is called once per frame
    void Update() {

        //bool grounded = CheckGrounded();
        grounded = CheckGrounded();

        endJump();
        
        if (!grounded)
        {
            mAnimator.SetBool("isFalling", true);
        }
        else
        {
            mAnimator.SetBool("isFalling", false);
        }

        if (mJumpCD)
        {
            mJumpTimer += Time.deltaTime;
            if (mJumpTimer >= jumpCooldDown)
            {
                mJumpCD = false;
                mJumpTimer = 0.0f;
            }
        }

        if (!mJumpCD && grounded && !mStun && Input.GetKey(KeyCode.W) && GetComponent<Rigidbody2D>().velocity.y < 0.1)
        {
            mJumping = true;
            mAnimator.SetBool("isFalling", false);
            //Debug.Log("My jump velocity when I jump is: " + mJumpForce);
            mRigidBody2D.AddForce(Vector2.up * mJumpForce, ForceMode2D.Impulse);
            mJumpCD = true;
        }


        mWalking = false;
		// Move left
		if(Input.GetKey(KeyCode.A) && !mStun)
		{
			transform.Translate (-Vector2.right * mMoveSpeed * Time.deltaTime);
			FaceDirection(-Vector2.right);
			mWalking = true;
		}
		// Move right
		if(Input.GetKey(KeyCode.D) && !mStun)
		{
			transform.Translate (Vector2.right * mMoveSpeed * Time.deltaTime);
			FaceDirection(Vector2.right);
			mWalking = true;
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
        if (!mJumping)
		    mAnimator.SetBool ("isWalking", mWalking);
        mAnimator.SetBool("isJumping", mJumping);
        mAnimator.SetBool ("isSwinging", GrapplingHook.swinging);
      
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
        mStun = true;
        Invoke("stunOff", 0.5f);
        if (!mInvincible)
        {
            Vector2 forceDirection = new Vector2(-mFacingDirection.x, 1.0f) * kDamagePushForce;
            mRigidBody2D.velocity = Vector2.zero;
            mRigidBody2D.AddForce(forceDirection, ForceMode2D.Impulse);
            mInvincible = true;
            life.loseHeart(dmg);
        }
    }

    void stunOff()
    {
        mStun = false;
    }

    public void HealHP(int health)
    {
        life.gainHeart(health);
    }
}
