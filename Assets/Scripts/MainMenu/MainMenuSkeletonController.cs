using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSkeletonController : MonoBehaviour {

    public GameObject mainMenuController;

    public void onSkeletonArrived(){
        mainMenuController.GetComponent<MainMenuController>().onSkeletonArrived();
    }
}