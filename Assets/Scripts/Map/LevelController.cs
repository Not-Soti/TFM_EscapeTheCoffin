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

    public GameObject emptyGunPrefab;
    public GameObject boneGunPrefab;
    public GameObject magicWandPrefab;
    private GameObject chosenWeapon;

    private LevelController instance;
    

    void Awake(){
        DontDestroyOnLoad(this);

        if(instance == null){
            instance = this;

            audioSource = GetComponent<AudioSource>();


            currentFloor = 0;

            audioSource.Stop();
            if(lobbyMusic != null){
                audioSource.clip = lobbyMusic;
                audioSource.Play();   
            }

            chosenWeapon = emptyGunPrefab;

        } else {
            Destroy(gameObject);
        }
        
    }

    public void onFloorFinished(){
        currentFloor++;

        var isMagicWandUnlocked = getIsMagicWandUnlocked();
        if(!isMagicWandUnlocked) {
            chosenWeapon = boneGunPrefab;
        } else {
            chosenWeapon = magicWandPrefab;
        }
        
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

            //Unlock new weapon the first time the boss is killed
            unlockMagicWand();

            resetFloors();
        }
    }

    public void resetFloors(){
        SceneManager.LoadScene("LobbyScene");
        Destroy(gameObject);
    }

    private void unlockMagicWand(){
        Debug.Log("STM - LevelController::unlockMagicWand");
        var storage = new UnlockablesStorage();
        storage.unlockMagicWand();
    }

    private bool getIsMagicWandUnlocked(){
        Debug.Log("STM - LevelController::getIsMagicWandUnlocked");
        var storage = new UnlockablesStorage();
        return storage.getIsMagicWandUnlocked();
    }

    public GameObject getChosenWeapon() {
        return chosenWeapon;
    }
    
}
