using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapController : MonoBehaviour
{

    public List<GameObject> availableRoomPrefabs; //Room prefabs used to build the level
    public int roomGeneratingDeepness;
    private List<Room> availableRooms;
    
    //private List<GameObject> levelRooms; //Rooms that conform the level
    //private List<Room> levelRooms; //Rooms that conform the level

    // Start is called before the first frame update
    void Start()
    {
        availableRooms = new List<Room>();
        foreach (var roomPrefab in availableRoomPrefabs)
        {
            availableRooms.Add(new Room(roomPrefab));
        }

/*
        //Create map
        for (int i = 0; i < numberOfRooms; i++)
        {
            int roomToInstantiate = Random.Range(0, availableRoomPrefabs.Count);
            levelRooms.Insert(i, availableRoomPrefabs.ElementAt(roomToInstantiate));
        }
*/
        Debug.LogFormat("MapController::Start - Creating map from a pool of {0} different rooms.", availableRooms.Count);
        Room initialRoom = availableRooms.ElementAt(Random.Range(0, availableRooms.Count));
        GameObject initialRoomGameObject = Instantiate(initialRoom.prefab, new Vector3(0, 0, 0), Quaternion.identity);
        initialRoom.setPrefab(initialRoomGameObject);
        instantiateRooms(initialRoom, roomGeneratingDeepness);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
        Generates new rooms recursively. Creates a random room and for each of its doors
        generates a new random room with matching doors.

        Repeat the process until getting a tree with @param generatingDeepness levels.
    */
    void instantiateRooms(Room currentRoom, int remainingDeepness)
    {
        if(remainingDeepness <= 0)
        {
            return;
        }

        foreach(Room.DoorDirection direction in currentRoom.getAvailableDirections())
        {
            Room newRoom = getMatchingRoom(direction);

            //Debug.LogFormat("STM - for room \n{0}\n generating room\n {1}", currentRoom.ToString(), newRoom.ToString());

            GameObject newRoomPrefab = Instantiate(newRoom.prefab, new Vector3(0,0,0), Quaternion.identity);
            newRoom.setPrefab(newRoomPrefab);

            GameObject currentRoomExitDoor = currentRoom.prefab.transform.Find(Room.GetDoorNameFromDirection(direction)).gameObject;
            GameObject newRoomEntryDoor = newRoom.prefab.transform.Find(Room.GetDoorNameFromDirection(Room.GetOppositeDirection(direction))).gameObject;

            Vector3 currentRoomDistanceCenterToExitDoor = currentRoom.prefab.transform.position - currentRoomExitDoor.transform.position;
            Vector3 newRoomDistanceCenterToEntryDoor = newRoom.prefab.transform.position - newRoomEntryDoor.transform.position;

            float newRoomX = Mathf.Abs(currentRoomDistanceCenterToExitDoor.x) + Mathf.Abs(newRoomDistanceCenterToEntryDoor.x);
            if(direction == Room.DoorDirection.Left){
                newRoomX *= -1;
            }

            float newRoomY = Mathf.Abs(currentRoomDistanceCenterToExitDoor.y) + Mathf.Abs(newRoomDistanceCenterToEntryDoor.y);
            if(direction == Room.DoorDirection.Bottom){
                newRoomY *= -1;
            }


            Vector3 newRoomPosition = 
                new Vector3(
                    newRoomX,
                    newRoomY,
                    0
                ) + currentRoom.prefab.transform.position;

                
            //Debug.LogFormat("STM - currentRoomDistanceCenterToExitDoor = {0}\n newRoomDistanceCenterToEntryDoor = {1}\n currentRoomPosition = {2}\n newRoomPosition = {3}", currentRoomDistanceCenterToExitDoor, newRoomDistanceCenterToEntryDoor, currentRoom.prefab.transform.position, newRoomPosition);

            newRoom.prefab.transform.position = newRoomPosition;
            instantiateRooms(newRoom, remainingDeepness - 1);
        }

    }

    /**
        Returns a random room from the available ones such that it is
        accessible from @param entryDirection Room.DoorDirection
    */
    private Room getMatchingRoom(Room.DoorDirection entryDirection) {
        List<Room> matchingRooms = availableRooms.Where(room => room.getAvailableDirections().Contains(Room.GetOppositeDirection(entryDirection))).ToList();
        
        return matchingRooms.ElementAt(Random.Range(0, matchingRooms.Count));
    }

/*
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
*/
}