using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject skeleton;
    public GameObject undertaker;

    public GameObject mainMenu;

    public void Start(){
        mainMenu.SetActive(false);
    }

    public void OnPlayButtonTapped() {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnExitButtonTapped() {
        Application.Quit();
    }

    public void OnClearDataTapped() {
        var storageManager = new StorageManager();
        storageManager.clearAll();
    }

    public void onSkeletonArrived(){
        undertaker.GetComponent<Animator>().SetTrigger("onSkeletonArrived");
    }

    public void onUndertakerArrived(){
        mainMenu.SetActive(true);
    }

}
