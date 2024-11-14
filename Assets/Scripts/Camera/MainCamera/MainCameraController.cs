using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{

    public GameObject mainCharacter;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = mainCharacter.transform.position.x;
        position.y = mainCharacter.transform.position.y;
        transform.position = position;
    }
}
