using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FinalBossMapController : MapController
{

    override public void Start(){
        Debug.Log("STM - Start");

        /*
        base.enemiesInScene = new List<GameObject>();

        var boss = GameObject.Find("Undertaker");
        if(boss != null){
            base.enemiesInScene.Add(boss.gameObject);
        }
        */
    }

    override public List<GameObject> getEnemiesInScene(){
        Debug.Log("STM - getting enemies");
        var boss = GameObject.Find("Undertaker");
        var list = new List<GameObject>();
        list.Add(boss.gameObject);
        Debug.LogFormat("STM - boss null = {0}", boss == null);
        return list;
    }
}