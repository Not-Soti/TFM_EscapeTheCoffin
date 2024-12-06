using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbarController : MonoBehaviour
{
    public GameObject enemy;

    private Slider slider;
    private int maxHealth;

    private void Awake(){
        slider = gameObject.GetComponent<Slider>();
        slider.value = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        var enemyController = enemy.GetComponent<EnemyController>();
        if(enemyController != null){
            maxHealth = enemyController.maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var enemyController = enemy.GetComponent<EnemyController>();
        if(enemyController != null){
            var currentHealth = enemyController.getCurrentHealth();
            slider.value = currentHealth/(float)maxHealth;
        }        
    }
}
