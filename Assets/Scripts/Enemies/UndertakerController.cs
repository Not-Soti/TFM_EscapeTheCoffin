using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndertakerController : EnemyController
{

    protected override void performAction() {
        float now = Time.time;
        float timeOut = Random.Range(1, 3);

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

        protected override void doFacePlayer(){
        if(player == null){
            var playerInScene = GameObject.Find("MainCharacterSkeleton");
            if(playerInScene != null){
                player = playerInScene.gameObject;
            }
        }
        Vector3 dir = player.transform.position - transform.position;

        var sprite = gameObject.transform.Find("undertaker");
        if(sprite != null){
            var currentScale = sprite.gameObject.transform.localScale;
            if (dir.x < 0f) {
                sprite.gameObject.transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
            } else {
                sprite.gameObject.transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
            }
        }
    }
}