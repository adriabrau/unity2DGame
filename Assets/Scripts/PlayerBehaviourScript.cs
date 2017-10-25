using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collisions2D))]
public class PlayerBehaviourScript : MonoBehaviour
{
    public enum State { Default, Dead, God}
    public State state;

    [Header("State")]
    public bool canMove = true;
    public bool canJump = true;
    public bool running = false;
    public bool isFacingRight = true;
    public bool isJumping = false;
    [Header("Physics")]
    public Rigidbody2D rb;
    public Collisions2D collisions;
    [Header("Speed propierties")]
    public float walkSpeed = 2;
    public float runSpeed = 3;
    public float movementSpeed;

    [Header("Force propierties")]
    public float jumpWalkForce;
    public float jumpRunForce;
    public float jumpForce;


    // Use this for initialization
    void Start ()
    {
        collisions = GetComponent<Collisions2D>();
        collisions.MyStart();
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(state)
        {
            case State.Default:
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

    }


}
