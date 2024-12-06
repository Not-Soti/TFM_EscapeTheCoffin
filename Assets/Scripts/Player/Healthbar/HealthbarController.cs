using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    public GameObject player;

    private Slider slider;
    private int maxHealth;

    private void Awake(){
        slider = gameObject.GetComponent<Slider>();
        slider.value = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        var playerController = player.GetComponent<PlayerController>();
        if(playerController != null){
            maxHealth = playerController.maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var playerController = player.GetComponent<PlayerController>();
        if(playerController != null){
            var currentHealth = playerController.getCurrentHealth();
            slider.value = currentHealth/(float)maxHealth;
        }        
    }
}
