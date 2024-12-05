using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private int currentFloor;
    public int maxFloors;
    public AudioSource audioSource;
    public AudioClip lobbyMusic;
    public AudioClip dungeonMusic;
    public AudioClip finalBossMusic;
    

    void Start(){
        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);
        currentFloor = 0;
    }

    public void onFloorFinished(){
        currentFloor++;
        Debug.LogFormat("STM - currentFloor = {0}", currentFloor);
        
        if(currentFloor < maxFloors){
            SceneManager.LoadScene("Level1");

            audioSource.Stop();
            if(dungeonMusic != null){
                audioSource.clip = dungeonMusic;
                audioSource.Play();   
            }

        } else if(currentFloor == maxFloors){
            SceneManager.LoadScene("FinalBoss");

            audioSource.Stop();
            if(finalBossMusic != null){
                audioSource.clip = finalBossMusic;
                audioSource.Play();   
            }
        } else if(currentFloor > maxFloors){
            resetFloors();
        }
    }

    public void resetFloors(){
        currentFloor = 0;
        SceneManager.LoadScene("LobbyScene");

        audioSource.Stop();
        if(lobbyMusic != null){
            audioSource.clip = lobbyMusic;
            audioSource.Play();   
        }
    }
}
