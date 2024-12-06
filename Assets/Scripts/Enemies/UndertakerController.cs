using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndertakerController : EnemyController
{

    protected override void performAction() {
        float now = Time.time;
        float timeOut = Random.Range(2, 5);

        //More % of attacking than a common enemy
        if(now > (lastMovementTimeSeconds + timeOut)) {
            int action = Random.Range(0, 5);
             Vector2 direction = new Vector2(0, 0);

            if(action == (int)EnemyAnimationState.Idle) {
                animator.SetInteger("action", (int) EnemyAnimationState.Idle);
                rigidBody.velocity = direction * speed;   

            } else if (action == (int)EnemyAnimationState.Running) {
                animator.SetInteger("action", (int) EnemyAnimationState.Running);
                direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                rigidBody.velocity = direction * speed;   
                
            } else {
                animator.SetInteger("action", (int) EnemyAnimationState.Attacking);
                rigidBody.velocity = direction * speed;   
            }

            lastMovementTimeSeconds = now;
        }
    }
}