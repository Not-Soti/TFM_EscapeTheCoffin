using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClosedCoffinController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("STM - ClosedCoffinController::OnTriggerEnter2D");
        PlayerController playerController = collider.GetComponent<PlayerController>();
        if(playerController != null){            
            var parent = GetComponentInParent<FinishLevelBeaconController>();
            if(parent != null){
                parent.openCoffin();
            }
        }
    }

}