using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayButtonTapped() {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnExitButtonTapped() {
        Application.Quit();
    }

}
