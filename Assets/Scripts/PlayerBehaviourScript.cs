using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Collisions2D))]
public class PlayerBehaviourScript : MonoBehaviour
{
    public enum State { Default, Dead, God }
    public State state;
    //public enum targetTransform { }
	[SerializeField] private Transform targetTransform;
    [Header("State")]
    public bool canMove = true;
    public bool canJump = true;
    public bool running = false;
    public bool isFacingRight = true;
    public bool isJumping = false;
    public bool attack = false;
    public bool isTouchingDeathGround = false;
    public bool canAttack = true;
    [Header("Physics")]
    public Rigidbody2D rb;
    public Collisions2D collisions;
    [Header("Speed properties")]
    public float walkSpeed = 2;
    public float runSpeed = 3;
    public float movementSpeed;
    [Header("Force properties")]
    public float jumpWalkForce;
    public float jumpRunForce;
    public float jumpForce;
    [Header("Movement")]
    public Vector2 axis;
    public float horizontalSpeed;
    [Header("Damage")]
    public int life = 3;
	public int damage = 1;
    public bool dead = false;

    //[Header("Transforms")]
    //public Transform flipTransform;
    [Header("Graphics")]
    public SpriteRenderer rend;
    private Animator anim;
	public Transform target;
	[SerializeField] private float distanceFromTarget;
	public Enemy enemy;

    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
        //target = GameObject.FindGameObjectsWithTag("Enemy");
        collisions = GetComponent<Collisions2D>();
        rb = GetComponent<Rigidbody2D>();
		enemy = GetComponent<Enemy>();
        collisions.Start();
        attack = false;
        isFacingRight = true;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Default:
                DefaultUpdate();
                break;
            case State.Dead:
                break;
            case State.God:
                break;
            default:
                break;
        }
    }

    void DefaultUpdate()
    {
        //Calcule el movimiento en horizontal
        HorizontalMovement();
        //Saltar

        //Anim
        anim.SetBool("isGrounded", collisions.isGrounded);
        anim.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("speedY", Mathf.Abs(rb.velocity.y));
		anim.SetInteger("life", life);
    }

    private void FixedUpdate()
    {
        collisions.FixedUpdate();

        if(isTouchingDeathGround)

        if(isJumping)
        {
            isJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //Aplicaremos el movimiento
        rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
    }

    void HorizontalMovement()
    {
        if(!canMove)
        {
            horizontalSpeed = 0;
            return;
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if(-0.1f < axis.x && axis.x < 0.1f)
        {
            horizontalSpeed = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            return;
        }

        if(collisions.isWalled)
        {
            if((isFacingRight && axis.x > 0.1f) || (!isFacingRight && axis.x < -0.1f))
            {
                horizontalSpeed = 0;
                return;
            }
        }

        if(isFacingRight && axis.x < -0.1f) Flip();
        if(!isFacingRight && axis.x > 0.1f) Flip();

        if(running) movementSpeed = runSpeed;
        else movementSpeed = walkSpeed;
        anim.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
        horizontalSpeed = axis.x * movementSpeed;
    }
    void VerticalMovement()
    {
        /*
         * bool lookingUp
         * bool lookingDown
         * bool crouch
         */
    }
    void Jump(float force)
    {
        jumpForce = force;
        isJumping = true;
    }
    void Flip()
    {
        rend.flipX = !rend.flipX;
        isFacingRight = !isFacingRight;
        collisions.Flip(isFacingRight);
    }
    void Attack()
    {
        anim.SetTrigger("attack");
		if (Vector3.Distance (transform.position, target.position) < 1f) 
		{
			targetTransform.GetComponent<Enemy> ().Damage (damage);
		} 

    }


    #region Public functions
    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
    }
    public void JumpStart()
    {
        if(!canJump) return;

        if(state == State.Default)
        {
            if(collisions.isGrounded)
            {
                if(running) Jump(jumpRunForce);
                else Jump(jumpWalkForce);
            }
        }

    }
    public void AttackStart()
    {
        if(!canAttack) return;

        if(state == State.Default)
        {
            Attack();
            
        }

    }
    public void Damage(int hit)
    {
        life -= hit;
        if(life <= 0)
        {
            life = 0;
            dead = true;
            SceneManager.LoadScene("manager2D");
        }
    }
	void CalculateDistanceFromTarget()
	{
		distanceFromTarget = Vector2.Distance(transform.position, targetTransform.position);
	}
    #endregion
}
