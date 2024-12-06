using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossDoorController : MonoBehaviour
{

    public GameObject boss;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.collider.GetComponent<PlayerController>();
        if(playerController != null){
            var level = GameObject.Find("LevelController").GetComponent<LevelController>();
            //level.onFloorFinished();       
            level.resetFloors();
        }
    }

}
