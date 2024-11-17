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
        if((shooter != null) && (target != null)){
            
            Vector3 shooterPosition = shooter.transform.position;
            Vector3 targetPosition = target.transform.position;
            
            this.transform.position = shooterPosition;
            
            Vector3 direction = targetPosition - shooterPosition;
            rigidBody.velocity = 20 *  direction.normalized; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        IBulletTarget target = collision.gameObject.GetComponent<IBulletTarget>();
        target.onShootReceived();        
    }
}