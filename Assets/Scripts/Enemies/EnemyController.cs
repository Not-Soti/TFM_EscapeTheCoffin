using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IBulletTarget
{

    private GameObject player;
    public float speed;
    public GameObject bulletPrefab;
    public GameObject targetMark;
    
    private float lastMovementTimeSeconds;

    private Animator animator;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();

        lastMovementTimeSeconds = Time.time;
        targetMark.SetActive(false);
    }

    public void initialize(GameObject player){
        this.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        doFacePlayer(); 
        performAction();
    }

    private void doFacePlayer(){
        if(player == null){
            var playerInScene = GameObject.Find("MainCharacterSkeleton");
            if(playerInScene != null){
                player = playerInScene.gameObject;
            }
        }
        Vector3 dir = player.transform.position - transform.position;
        if (dir.x < 0f) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void performAction() {
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

    public void shootBullet() {
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(0,0,0), Quaternion.identity);
        bullet.GetComponent<BulletController>().initialize(gameObject, player);
        bullet.GetComponent<BulletController>().shoot();    
    }

    public void onShootReceived() {
        GameObject map = GameObject.Find("Map");
        if(map != null) {
            List<GameObject> enemies = map.GetComponent<MapController>().getEnemiesInScene();

            enemies.Remove(gameObject);
        }
        Destroy(gameObject);
    }

    public void setAsClosestEnemy(bool isClosestEnemy){
        targetMark.SetActive(isClosestEnemy);
    }

    private enum EnemyAnimationState {
        Idle = 0,
        Running = 1,
        Attacking = 2,
    }
}
