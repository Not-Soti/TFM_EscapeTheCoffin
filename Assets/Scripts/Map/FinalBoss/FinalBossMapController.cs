using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FinalBossMapController : MapController
{

    public GameObject doorOpen;
    public GameObject doorClosed;

    override public void Start(){
        
    }

    override public void Update() {
        checkDoor();
    }

    override public List<GameObject> getEnemiesInScene(){
        var boss = GameObject.Find("Undertaker");
        var list = new List<GameObject>();
        if(boss != null){
            list.Add(boss.gameObject);
        }
        return list;
    }

    private void checkDoor(){
        var boss = GameObject.Find("Undertaker");
        if(boss == null){
            doorClosed.SetActive(false);
            doorOpen.SetActive(true);
        }
    }
}