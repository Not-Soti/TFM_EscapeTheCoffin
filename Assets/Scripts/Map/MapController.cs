using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapController : MonoBehaviour
{

    public List<GameObject> roomPrefabs; //Room prefabs used to build the level
    
    private List<GameObject> levelRooms; //Rooms that conform the level

    // Start is called before the first frame update
    void Start()
    {
        int numberOfRooms = Random.Range(3, 10);
        levelRooms = new List<GameObject>(numberOfRooms);

        //Create map
        for (int i = 0; i < numberOfRooms; i++)
        {
            int roomToInstantiate = Random.Range(0, roomPrefabs.Count);
            levelRooms.Insert(i, roomPrefabs.ElementAt(roomToInstantiate));
        }

        instantiateRooms();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void instantiateRooms(){
                //Instantiate the map
        for (int i = 0; i < levelRooms.Count; i++)
        {
            GameObject actualRoom = Instantiate(levelRooms.ElementAt(i), new Vector3(0,0,0), Quaternion.identity);
 
            //Place rooms relative to the previous one
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
                actualRoom.transform.position = newPosition;
            }
            
            levelRooms[i] = actualRoom;
        }
    }
}