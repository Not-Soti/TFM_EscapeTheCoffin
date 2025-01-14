using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private int currentFloor;
    public int maxFloors;
    public float volume;

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
            audioSource.volume = volume;

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
        var storage = new UnlockablesStorage();
        storage.unlockMagicWand();
    }

    private bool getIsMagicWandUnlocked(){
        var storage = new UnlockablesStorage();
        return storage.getIsMagicWandUnlocked();
    }

    public GameObject getChosenWeapon() {
        return chosenWeapon;
    }

    public void setChosenWeapon(string name){
        if(name.Equals("BoneGun")){
            chosenWeapon = boneGunPrefab;
        } else if(name.Equals("MagicWand")){
            chosenWeapon = magicWandPrefab;
        }

        var player = GameObject.Find("MainCharacter").GetComponent<PlayerController>();
        player.instantiateWeapon();
    }
    
}
