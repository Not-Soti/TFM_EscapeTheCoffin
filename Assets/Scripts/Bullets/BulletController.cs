using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletController : MonoBehaviour {

    public float rotationSpeed;

    private GameObject shooter;
    private GameObject target;
    private Rigidbody2D rigidBody;

    void Update(){
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    public void initialize(GameObject shooter, GameObject target){
        rigidBody = GetComponent<Rigidbody2D>(); 
        
        this.shooter = shooter;
        this.target = target;

        Collider2D shooterCollider = this.shooter.GetComponent<Collider2D>();
        Collider2D bulletCollider = GetComponent<Collider2D>();

        if (shooterCollider != null && bulletCollider != null)
        {
            Physics2D.IgnoreCollision(shooterCollider, bulletCollider);
        }
    }

    public void shoot() {
        if(target != null){
            //Shoot the enemy
            Vector3 shooterPosition = shooter.transform.position;
            Vector3 targetPosition = target.transform.position;
            
            this.transform.position = shooterPosition;
            
            Vector3 direction = targetPosition - shooterPosition;
            rigidBody.velocity = 20 *  direction.normalized; 
        } else {
            //Straight shoot
            Vector3 shooterPosition = shooter.transform.position;
            transform.position = shooterPosition;
            Vector3 direction = new Vector3(1, 0, 0);
            if(shooter.transform.localScale.x == 1){
                direction = new Vector3(-1, 0, 0);
            }
            rigidBody.velocity = 20 *  direction; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        IBulletTarget target = collision.gameObject.GetComponent<IBulletTarget>();
        target.onShootReceived();        
    }
}