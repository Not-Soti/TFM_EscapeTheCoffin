using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doFacePlayer(); 
    }

    private void doFacePlayer(){
        Vector3 dir = player.transform.position - transform.position;
        if (dir.x < 0f) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private enum EnemyAnimationState {
        Idle = 0,
        Running = 1,
        Attacking,
    }
}
