using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : EnemyController
{
    override public void Start(){
        base.Start();

        base.animator = gameObject.transform.Find("Ghost").GetComponent<Animator>();
    }

    override protected void performAction() {
        float now = Time.time;
        float timeOut = Random.Range(2, 5);

        if(now > (lastMovementTimeSeconds + timeOut)) {
            int action = Random.Range(0, 3);
             Vector2 direction = new Vector2(0, 0);
            
            switch((EnemyAnimationState)action){
                case EnemyAnimationState.Idle:
                    animator.SetInteger("action", (int) EnemyAnimationState.Idle);
                   
                    rigidBody.velocity = direction * speed;   
                    break;
                case EnemyAnimationState.Running:
                    animator.SetInteger("action", (int) EnemyAnimationState.Running);
                    direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    rigidBody.velocity = direction * speed;   
                    break;
                case EnemyAnimationState.Attacking:
                    animator.SetInteger("action", (int) EnemyAnimationState.Attacking);
                    rigidBody.velocity = direction * speed;   
                    break;
                default:
                    Debug.Log("EnemyController::performAction - Unknown action");
                    break;
            }

            lastMovementTimeSeconds = now;
        }
    }

}