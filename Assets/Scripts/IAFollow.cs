using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IAFollow : MonoBehaviour

{

    public Transform target;//set target from inspector instead of looking in Update
    public Transform target2;
    public float speed = 3f;
    public bool isFacingRight = true;
    public SpriteRenderer rend;
    public bool attack = false;
    public bool canAttack = true;
    private Animator anim;
    public float timecounter;
	public PlayerBehaviourScript player;

    void Start()
    {
		player = GetComponent<PlayerBehaviourScript>();
        isFacingRight = true;
        anim = GetComponent<Animator>();
        attack = false;
        timecounter = Time.deltaTime;
    }

    void Update()
    {

        //rotate to look at the player
        //transform.LookAt(target.position);
        //transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
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
                Attack();
            }
            else { }
            
        }

    }
    void Flip()
    {
        rend.flipX = !rend.flipX;
        isFacingRight = !isFacingRight;
    }
    void Attack()
    {
        anim.SetTrigger("attack");
		player.life -= 1;
    }
}
