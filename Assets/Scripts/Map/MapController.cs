using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapController : MonoBehaviour
{

    public GameObject[] roomPrefabs; //Room prefabs used to build the level
    
    private List<GameObject> levelRooms; //Rooms that conform the level

    // Start is called before the first frame update
    void Start()
    {
        int numberOfRooms = Random.Range(3, 10);
        levelRooms = new List<GameObject>(numberOfRooms);

        //Create map
        for (int i = 0; i < numberOfRooms; i++)
        {
            int roomToInstantiate = Random.Range(0, roomPrefabs.Length);
            levelRooms.Insert(i, roomPrefabs[roomToInstantiate]);
        }


        //Instantiate the map
        for (int i = 0; i < levelRooms.Count; i++)
        {
            GameObject actualRoom = Instantiate(levelRooms.ElementAt(i), new Vector3(0,0,0), Quaternion.identity);

            if(i > 0)
            {
                GameObject previousRoom = levelRooms.ElementAt(i-1);
                GameObject previousRoomLeftDoor = previousRoom.transform.Find("DoorLeft").gameObject;
                Vector3 previousRoomDistanceCenterToLeftDoor = previousRoom.transform.position - previousRoomLeftDoor.transform.position;

                GameObject actualRoomRightDoor = actualRoom.transform.Find("DoorRight").gameObject;
                Vector3 actualRoomDistanceCenterToRightDoor = actualRoom.transform.position - actualRoomRightDoor.transform.position;

                Vector3 newPosition = new Vector3(
                    Mathf.Abs(previousRoomDistanceCenterToLeftDoor.x) + Mathf.Abs(actualRoomDistanceCenterToRightDoor.x),
                    Mathf.Abs(previousRoomDistanceCenterToLeftDoor.y) + Mathf.Abs(actualRoomDistanceCenterToRightDoor.y),
                    0
                ) + previousRoom.transform.position;
                Debug.LogFormat("STM - \npreviousRoomDistanceCenterToLeftDoor = {0} \nactualRoomDistanceCenterToRightDoor = {1} \nnewPosition = {2}", previousRoomDistanceCenterToLeftDoor, actualRoomDistanceCenterToRightDoor, newPosition);
                actualRoom.transform.position = newPosition;
            }
            
            levelRooms[i] = actualRoom;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
