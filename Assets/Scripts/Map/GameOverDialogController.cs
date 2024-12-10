using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTextController : MonoBehaviour
{
    public GameObject player;
    
    private CanvasGroup canvasGroup; 
    private bool isShowing;

    void Start(){
        canvasGroup = GetComponent<CanvasGroup>();        
        isShowing = false;
        hide();
    }

    void Update(){
        if(player.GetComponent<PlayerController>().getCurrentHealth() <= 0){
            show();
        }
    }

    private void hide(){        
        canvasGroup.blocksRaycasts = false; 
        canvasGroup.alpha = 0;

        Time.timeScale = 1;
    }

    private void show(){
        if(!isShowing){
            isShowing = true;
            canvasGroup.blocksRaycasts = true; 
            canvasGroup.alpha = 1;

            Time.timeScale = 0;
        }
    }

    public void onClick(){
        hide();
        var level = GameObject.Find("LevelController").GetComponent<LevelController>();
        level.resetFloors();           
    }
}