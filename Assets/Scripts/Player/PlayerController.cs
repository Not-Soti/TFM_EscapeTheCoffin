using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick movementJoystick;
    public float moveSpeed = 5.0f;

    public GameObject idlePrefab;
    public GameObject runningPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();

        //idlePrefab = Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        idlePrefab.SetActive(true);
        //runningPrefab = Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        runningPrefab.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 inputDirection = movementJoystick.Direction;
        if(inputDirection != new Vector2(0f, 0f)){
            animator.SetInteger("player_animation_state", (int) PlayerAnimationState.Walking);

            idlePrefab.SetActive(false);
            runningPrefab.SetActive(true);

            int facingDirection = -1;
            if(inputDirection.x < 0) {
                facingDirection = 1;
            }
            transform.localScale = new Vector3(facingDirection, 1f, 1f);
        }else {
            animator.SetInteger("player_animation_state", (int) PlayerAnimationState.Idle);
            idlePrefab.SetActive(true);
            runningPrefab.SetActive(false);
        }

        //Raycast to check Start game door collider
        Debug.DrawRay(transform.position, Vector3.up * 1f, Color.red);
        if(Physics2D.Raycast(transform.position, Vector3.up, 1f)){
           // Debug.Log("STM - Raycast activated");
        }
    }

    void FixedUpdate()
    {
        Vector2 inputDirection = movementJoystick.Direction;
        rigidBody.velocity = inputDirection * moveSpeed;   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartGameDoorController doorController = collision.collider.GetComponent<StartGameDoorController>();
        if(doorController != null){
            Debug.Log("STM - puerta");
        } else {
            Debug.Log("STM - NO puerta");
        }
    }


    //MARK: Private

    private Rigidbody2D rigidBody;
    private Animator animator;
    
    private enum PlayerAnimationState {
        Idle = 0,
        Walking = 1
    }

}
