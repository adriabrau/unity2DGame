using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{

    [SerializeField] private Transform targetTransform;
    enum EnemyState { Idle, Chase, Attack, Stun, Dead };
    [SerializeField] EnemyState state;



    [Header("Timers")]
    public float idleTime = 1;
    public float stunTime = 1;
    public float coolDownAttack = 0.3f;
    private float timeCounter = 0;

    [Header("Distances")]
    public float chaseRange;
    public float attackRange;
    [SerializeField] private float distanceFromTarget;

    [Header("Booleans")]
    private bool canAttack = false;

    [Header("Properties")]
    public int damage = 1;
    public int life = 1;


	public Transform target;//set target from inspector instead of looking in Update
	public Transform target2;
	public float speed = 3f;
	public bool isFacingRight = true;
	public SpriteRenderer rend;
	public bool attack = false;
	public float timecounter;
	public PlayerBehaviourScript player;
	private Animator anim;
    //public AudioSource deadSound;



    void Start()
    {

        //targetTransform = GameObject.Find("Player").transform;
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
		player = GetComponent<PlayerBehaviourScript>();
		isFacingRight = true;
		anim = GetComponent<Animator>();
		attack = false;
		timecounter = Time.deltaTime;
        SetIdle();
    }

    void OnEnable()
    {

        life = 1;
        SetIdle();
    }

    void Update()
    {
        //agent.SetDestination(targetTransform.position);
		anim.SetInteger("life", life);
        CalculateDistanceFromTarget();
        switch(state)
        {
            case EnemyState.Idle:
                IdleUpdate();
                break;
            case EnemyState.Chase:
                ChaseUpdate();
                break;
            case EnemyState.Attack:
                AttackUpdate();
                break;
            case EnemyState.Stun:
                StunUpdate();
                break;
            case EnemyState.Dead:
                DeadUpdate();
                break;
        }

    }

    #region Updates
    void IdleUpdate()
    {
        if(timeCounter >= idleTime)
        {
			SetChase();
			return;
        }
        else timeCounter += Time.deltaTime;
    }

    void ChaseUpdate()
    {
		if(isFacingRight && target.position.x < target2.position.x - 0.1f) Flip();
		if(!isFacingRight && target.position.x > target2.position.x + 0.1f) Flip();
		transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
		//move towards the player
		if(Vector3.Distance(transform.position, target.position) > 5f)
		{//move if distance from target is greater than 1
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		}
		if(Vector3.Distance(transform.position, target.position) < 1f)
		{
			//attack
			timecounter = 2;
			if(Vector3.Distance(transform.position, target.position) < 1f)
			{
				
				Debug.Log("Atakerl");
				anim.SetTrigger("attack");
				SetAttack ();
			}
			else { }

		}
    }
    void AttackUpdate()
    {
		if (Vector3.Distance (transform.position, target.position) < 1f) {
			targetTransform.GetComponent<PlayerBehaviourScript> ().Damage (damage);
			idleTime = coolDownAttack;
			SetIdle();
		} 
		else 
		{
			SetIdle();
		}
        


        if(distanceFromTarget > attackRange)
        {
            SetChase();
            return;
        }

    }
    void StunUpdate()
    {
        if(timeCounter >= stunTime)
        {
            idleTime = 0;
            SetIdle();
        }
        else timeCounter += Time.deltaTime;
    }
    void DeadUpdate()
    {
        stunTime = 6;
        anim.SetTrigger("dead");
        stunTime = 6;
    }
    #endregion
    #region Sets
    void SetIdle()
    {
        timeCounter = 0;
        //Animacion

        state = EnemyState.Idle;
    }
	void Flip()
	{
		rend.flipX = !rend.flipX;
		isFacingRight = !isFacingRight;
	}
    void SetChase()
    {
        state = EnemyState.Chase;
    }
    void SetAttack()
    {
        state = EnemyState.Attack;
    }
    void SetStun()
    {
        timeCounter = 0;
        state = EnemyState.Stun;
    }
    void SetDead()
    {
        //deadSound.Play();

        anim.SetTrigger("dead");
        stunTime = 6;
        state = EnemyState.Dead;
        //Destroy(this.gameObject, 1);
        
    }
    #endregion

    #region Public Functions
    public void Damage(int hit)
    {
        life -= hit;
        if(life <= 0)
        {
            SetDead();
        }
        else
        {
            SetStun();
        }
    }
    #endregion


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Color newColor = Color.red;
        newColor.a = 0.2f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void CalculateDistanceFromTarget()
    {
        distanceFromTarget = Vector2.Distance(transform.position, targetTransform.position);
    }



}
