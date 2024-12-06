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

    private LevelController instance;
    

    void Awake(){
        Debug.Log("STM - Awake");
        DontDestroyOnLoad(this);

        if(instance == null){
            Debug.Log("STM - instance == null");
            instance = this;

            audioSource = GetComponent<AudioSource>();


            currentFloor = 0;

            audioSource.Stop();
            if(lobbyMusic != null){
                audioSource.clip = lobbyMusic;
                audioSource.Play();   
            }
        } else {
            Debug.Log("STM - instance NOT null");
            Destroy(gameObject);
        }
        
    }

    public void onFloorFinished(){
        currentFloor++;
        Debug.LogFormat("STM - currentFloor = {0}", currentFloor);
        
        if(currentFloor < maxFloors){
            SceneManager.LoadScene("Level1");

            if(audioSource.clip != dungeonMusic){
                audioSource.Stop();
                if(dungeonMusic != null){
                    audioSource.clip = dungeonMusic;
                    audioSource.Play();   
                }
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
        Debug.Log("STM - reset floors");
        currentFloor = 0;
        SceneManager.LoadScene("LobbyScene");

        audioSource.Stop();
        if(lobbyMusic != null){
            audioSource.clip = lobbyMusic;
            audioSource.Play();   
        }
    }
}
