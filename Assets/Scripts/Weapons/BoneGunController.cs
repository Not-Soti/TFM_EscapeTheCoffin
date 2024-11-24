using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneGunController : MonoBehaviour, IWeaponController {

    public GameObject bulletPrefab;

    void Start(){
        
    }

    public void shootBullet(GameObject shooter, GameObject target) {
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(0,0,0), Quaternion.identity);
        bullet.GetComponent<BulletController>().initialize(shooter, target);
        bullet.GetComponent<BulletController>().shoot();    
    }
}