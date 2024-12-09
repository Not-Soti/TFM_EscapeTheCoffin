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

        Debug.LogFormat("STM - current {0} - last {1} > speed {2}", currentShootTimestamp, lastShootTimeSeconds, firingSpeedMillis);
        if((currentShootTimestamp - lastShootTimeSeconds) > (firingSpeedMillis / 1000f)){
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(0,0,0), Quaternion.identity);
            bullet.GetComponent<BulletController>().initialize(shooter, target, bulletSpeed);
            bullet.GetComponent<BulletController>().shoot();    

            lastShootTimeSeconds = currentShootTimestamp;
        }
    }
}