using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneGunController : IWeaponController {

    public GameObject bulletPrefab;

    override public void shootBullet(GameObject shooter, GameObject target) {
        float currentShootTimestamp = Time.time;

        Debug.LogFormat("STM - current {0} - last {1} > speed {2}", currentShootTimestamp, base.lastShootTimeSeconds, base.firingSpeedMillis);
        if((currentShootTimestamp - base.lastShootTimeSeconds) > (base.firingSpeedMillis / 1000)){
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(0,0,0), Quaternion.identity);
            bullet.GetComponent<BulletController>().initialize(shooter, target);
            bullet.GetComponent<BulletController>().shoot();    

            base.lastShootTimeSeconds = currentShootTimestamp;
        }
    }
}