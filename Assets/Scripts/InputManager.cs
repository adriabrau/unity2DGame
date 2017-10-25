using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerBehaviourScript player;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviourScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        InputPause();
        InputRun();
        InputJump();
        InputMovement();
    }
    private void InputMovement()
    {
        Vector2 axis = Vector2.zero;
        axis.x = Input.GetAxis("Horizontal");
        axis.y = Input.GetAxis("Vertical");
    }
    private void InputRun()
    {
        if(Input.GetButtonDown("run")) player.running = true;
        else if(Input.GetButtonDown("run")) player.running = false;
    }
    private void InputJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            player.JumpStart();
        }
    }
    private void InputPause()
    {
        if(Input.GetButtonDown("Cancel")) Debug.Log("pausegame");
    }
}
