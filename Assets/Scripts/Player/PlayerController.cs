using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour, IBulletTarget
{
    public float moveSpeed = 5.0f;
    public int maxHealth;
    private int currentHealth;

    public Joystick movementJoystick;
    private bool isJoystickFloating;

    public GameObject idlePrefab;
    public GameObject runningPrefab;
    private GameObject instantiatedWeapon = null;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private GameObject closestEnemy;

    // Start is called before the first frame update
    void Start(){
        rigidBody = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        closestEnemy = null;
        isJoystickFloating = false;

        idlePrefab.SetActive(true);
        runningPrefab.SetActive(false);

        currentHealth = maxHealth;

        instantiateWeapon();
        
    }

    // Update is called once per frame
    void Update(){
        Vector2 inputDirection = movementJoystick.Direction;
        if(inputDirection != new Vector2(0f, 0f)){
            var variableJoystick = (movementJoystick as VariableJoystick);
            if(variableJoystick != null && !isJoystickFloating){
                variableJoystick.SetMode(JoystickType.Floating);
                isJoystickFloating = true;  
            }
            

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

        updateClosestEnemy();
        updateFacingDirection();
        updateWeaponPosition();
    }

    void FixedUpdate() {
        Vector2 inputDirection = movementJoystick.Direction;
        rigidBody.velocity = inputDirection * moveSpeed;   
    }

    public void instantiateWeapon(){
        if(instantiatedWeapon != null){
            Destroy(instantiatedWeapon);
        }
        var level = GameObject.Find("LevelController").GetComponent<LevelController>();
        var chosenWeapon = level.getChosenWeapon();
        instantiatedWeapon = Instantiate(chosenWeapon, new Vector3(0,0,0), Quaternion.identity);
    }

    public void onShootReceived() {
        Debug.Log("PlayerController::onShootReceived");
        currentHealth--;
    }

    private void updateWeaponPosition(){
        GameObject weaponHolderBone = null;

        if(GameObject.Find("weapon_holder_point")) {
            GameObject bone = GameObject.Find("weapon_holder_point").gameObject;
            var weaponScale = instantiatedWeapon.transform.localScale;

            instantiatedWeapon.transform.position = bone.transform.position;
            instantiatedWeapon.transform.rotation = bone.transform.rotation;
            instantiatedWeapon.transform.localScale = new Vector3(-gameObject.transform.localScale.x, weaponScale.y, weaponScale.z);
        }
    }

    public void shootBullet() {
        instantiatedWeapon.GetComponent<IWeaponController>().shootBullet(gameObject, closestEnemy);
    }

    private void updateClosestEnemy() {
        if(instantiatedWeapon == null) {
            return;
        }
        
        int autoAimDistance = instantiatedWeapon.GetComponent<IWeaponController>().range;
        GameObject map = GameObject.Find("Map");
        if(map != null) {
            List<GameObject> enemies = map.GetComponent<MapController>().getEnemiesInScene();

            if(enemies.Count == 0){
                closestEnemy = null;
            } else {
                GameObject enemy = enemies.OrderBy(enemy => Vector3.Distance(gameObject.transform.position, enemy.transform.position)).FirstOrDefault();

                if(closestEnemy != null && enemy != closestEnemy){
                    closestEnemy.GetComponent<EnemyController>().setAsClosestEnemy(false);
                }
                
                if(Vector3.Distance(gameObject.transform.position, enemy.transform.position) <= autoAimDistance){         
                    enemy.GetComponent<EnemyController>().setAsClosestEnemy(true);         
                    closestEnemy = enemy;

                    //Debug 
                    var raycastHeading = (enemy.transform.position - gameObject.transform.position);
                    var raycastDistance = raycastHeading.magnitude;
                    var raycastDirection = raycastHeading / raycastDistance;
                    Debug.DrawRay(gameObject.transform.position, raycastDirection * autoAimDistance, Color.red);
                } else {
                    enemy.GetComponent<EnemyController>().setAsClosestEnemy(false);
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

    public int getCurrentHealth(){
        return currentHealth;
    }
}
