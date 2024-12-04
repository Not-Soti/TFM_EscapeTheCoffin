using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private int currentFloor;
    public int maxFloors;

    void Start(){
        DontDestroyOnLoad(this.gameObject);
        currentFloor = 1;
    }

    public void onFloorFinished(){
        if(currentFloor < maxFloors){
            SceneManager.LoadScene("Level1");
            currentFloor++;
        } else {
            SceneManager.LoadScene("LobbyScene");
            resetFloors();
        }
    }

    public void resetFloors(){
        currentFloor = 1;
    }
}
