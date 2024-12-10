using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWeaponController : MonoBehaviour {
    
    public GameObject bulletPrefab;

    public int firingSpeedMillis;
    public int bulletSpeed;

    protected float lastShootTimeSeconds;

    void Start(){
        lastShootTimeSeconds = Time.time;
    } 
    
    public void shootBullet(GameObject shooter, GameObject target) {
        float currentShootTimestamp = Time.time;

        if((currentShootTimestamp - lastShootTimeSeconds) > (firingSpeedMillis / 1000f)){
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(0,0,0), Quaternion.identity);
            bullet.GetComponent<BulletController>().initialize(shooter, target, bulletSpeed);
            bullet.GetComponent<BulletController>().shoot();    

            lastShootTimeSeconds = currentShootTimestamp;
        }
    }
}