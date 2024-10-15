using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Joystick movementJoystick;
    public float moveSpeed = 5.0f;

    private Rigidbody2D rigidBody;
/*
    private Animator animator;
*/

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); 
        //animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 inputDirection = movementJoystick.Direction;
        if(inputDirection != new Vector2(0f, 0f)){
            //animator.SetInteger("player_animation_state", (int) PlayerAnimationState.Walking);

            int facingDirection = -1;
            if(inputDirection.x < 0) {
                facingDirection = 1;
            }
            transform.localScale = new Vector3(facingDirection, 1f, 1f);
        }else {
            //animator.SetInteger("player_animation_state", (int) PlayerAnimationState.Idle);
        }
    }

    void FixedUpdate()
    {
        Vector2 inputDirection = movementJoystick.Direction;
        rigidBody.velocity = inputDirection * moveSpeed;   
    }


    private enum PlayerAnimationState {
        Idle = 0,
        Walking = 1
    }

}
