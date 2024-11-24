using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour, IBulletTarget
{
    public Joystick movementJoystick;
    public float moveSpeed = 5.0f;

    public GameObject idlePrefab;
    public GameObject runningPrefab;
    public GameObject weaponPrefab;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private GameObject closestEnemy;

    // Start is called before the first frame update
    void Start(){
        rigidBody = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        closestEnemy = null;

        idlePrefab.SetActive(true);
        runningPrefab.SetActive(false);
    }


    // Update is called once per frame
    void Update(){
        Vector2 inputDirection = movementJoystick.Direction;
        if(inputDirection != new Vector2(0f, 0f)){

            idlePrefab.SetActive(false);
            runningPrefab.SetActive(true);

            int facingDirection = -1;
            if(inputDirection.x < 0) {
                facingDirection = 1;
            }
            transform.localScale = new Vector3(facingDirection, 1f, 1f);
        }else {
            idlePrefab.SetActive(true);
            runningPrefab.SetActive(false);
        }

        updateWeaponPosition();
        updateClosestEnemy();
        updateFacingDirection();
    }

    void FixedUpdate() {
        Vector2 inputDirection = movementJoystick.Direction;
        rigidBody.velocity = inputDirection * moveSpeed;   
    }

    public void onShootReceived() {
        Debug.Log("PlayerController::onShootReceived");
    }

    private void updateWeaponPosition(){
        GameObject weaponHolderBone = null;

        if(GameObject.Find("weapon_holder_point")) {
            GameObject bone = GameObject.Find("weapon_holder_point").gameObject;
            weaponPrefab.transform.position = bone.transform.position;
            weaponPrefab.transform.rotation = bone.transform.rotation;
        }
    }

    public void shootBullet() {
        weaponPrefab.GetComponent<IWeaponController>().shootBullet(gameObject, closestEnemy);
    }

    private void updateClosestEnemy() {
        int autoAimDistance = 6;
        GameObject map = GameObject.Find("Map");
        if(map != null) {
            List<GameObject> enemies = map.GetComponent<MapController>().getEnemiesInScene();

            if(enemies.Count == 0){
                closestEnemy = null;
            } else {
                GameObject enemy = enemies.OrderBy(enemy => Vector3.Distance(gameObject.transform.position, enemy.transform.position)).FirstOrDefault();
                if(Vector3.Distance(gameObject.transform.position, enemy.transform.position) <= autoAimDistance){
                    closestEnemy = enemy;

                    //Debug 
                    var raycastHeading = (enemy.transform.position - gameObject.transform.position);
                    var raycastDistance = raycastHeading.magnitude;
                    var raycastDirection = raycastHeading / raycastDistance;
                    Debug.DrawRay(gameObject.transform.position, raycastDirection * autoAimDistance, Color.red);
                } else {
                    closestEnemy = null;
                }
            }
        }
    }

    private void updateFacingDirection(){
        if(closestEnemy != null){
            int facingDirection = -1;

            if(closestEnemy.transform.position.x < transform.position.x) {
                facingDirection = 1;
            }
            transform.localScale = new Vector3(facingDirection, 1f, 1f);
        }
    } 
}
