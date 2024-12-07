using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBulletTriger : MonoBehaviour
{
    public GameObject ghost;

    public void triggerShoot(){
        var controller = ghost.GetComponent<EnemyController>();
        controller.shootBullet();
    }
}