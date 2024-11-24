using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IBulletTarget
{
    public Joystick movementJoystick;
    public float moveSpeed = 5.0f;

    public GameObject idlePrefab;
    public GameObject runningPrefab;
    public GameObject weaponPrefab;
    //public GameObject bulletPrefab;


    private Rigidbody2D rigidBody;
    private Animator animator;

    // Start is called before the first frame update
    void Start(){
        rigidBody = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();

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

        Debug.DrawRay(transform.position, Vector3.up * 1f, Color.red);
        if(Physics2D.Raycast(transform.position, Vector3.up, 1f)){
           // Debug.Log("STM - Raycast activated");
        }

        updateWeaponPosition();
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
        weaponPrefab.GetComponent<IWeaponController>().shootBullet(gameObject, null);
    }
}
