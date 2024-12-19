using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUndertakerController : MonoBehaviour {

    public GameObject mainMenuController;

    public void onUndertakerArrived(){
        mainMenuController.GetComponent<MainMenuController>().onUndertakerArrived();
    }
}