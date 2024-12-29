using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishLevelBeaconController : MonoBehaviour
{

    public UnityEvent onReach;
    public GameObject closed;
    public GameObject open;

    // Start is called before the first frame update
    void Start()
    {
        closed.SetActive(true);
        open.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openCoffin(){
        Debug.Log("STM - FinishLevelBeaconController::openCoffin");
        closed.SetActive(false);
        open.SetActive(true);
    }

    public void loadNextLevel(){
        Debug.Log("STM - FinishLevelBeaconController::loadNextLevel");
        var level = GameObject.Find("LevelController").GetComponent<LevelController>();
        level.onFloorFinished();            
    } 
    
}
