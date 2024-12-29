using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenCoffinController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("STM - OpenCoffinController::OnTriggerEnter2D");
        PlayerController playerController = collider.GetComponent<PlayerController>();
        if(playerController != null){ 
            var parent = GetComponentInParent<FinishLevelBeaconController>();
            if(parent != null){
                parent.loadNextLevel();
            }
        }
    }

}