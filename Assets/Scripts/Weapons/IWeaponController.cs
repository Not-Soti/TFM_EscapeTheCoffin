using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWeaponController : MonoBehaviour {
    
    public int firingSpeedMillis;

    protected float lastShootTimeSeconds;

    void Start(){
        lastShootTimeSeconds = Time.time;
    } 
    
    abstract public void shootBullet(GameObject shooter, GameObject target);
}