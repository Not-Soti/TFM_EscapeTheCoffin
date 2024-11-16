using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapController : MonoBehaviour
{

    public List<GameObject> availableRoomPrefabs; //Room prefabs used to build the level
    public int roomGeneratingDeepness;
    private List<Room> availableRooms;

    // Stores the map as a matrix, storing the relative coordinates of the matrix where
    // a room is and its value.
    private Dictionary<Vector2Int, Room> placedRooms;
    
    //private List<GameObject> levelRooms; //Rooms that conform the level
    //private List<Room> levelRooms; //Rooms that conform the level

    // Start is called before the first frame update
    void Start()
    {
        availableRooms = new List<Room>();
        placedRooms = new Dictionary<Vector2Int, Room>();

        foreach (var roomPrefab in availableRoomPrefabs)
        {
            availableRooms.Add(new Room(roomPrefab));
        }

        Debug.LogFormat("MapController::Start - Creating map from a pool of {0} different rooms.", availableRooms.Count);

        Room initialRoom = availableRooms.ElementAt(Random.Range(0, availableRooms.Count));
        GameObject initialRoomGameObject = Instantiate(initialRoom.prefab, new Vector3(0, 0, 0), Quaternion.identity);
        initialRoom.setPrefab(initialRoomGameObject);
        Vector2Int initialRoomPosition = new Vector2Int(0, 0);
        placedRooms[initialRoomPosition] = initialRoom;
        instantiateRooms(initialRoom, initialRoomPosition, roomGeneratingDeepness);
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
    void instantiateRooms(Room currentRoom, Vector2Int currentRoomPositionInMap, int remainingDeepness)
    {
        if(remainingDeepness <= 0)
        {
            return;
        }

        foreach(Room.DoorDirection direction in currentRoom.getAvailableDirections())
        {
            Room newRoom = getMatchingRoom(direction);   

            Vector2Int newRoomPositionInMap = currentRoomPositionInMap;
            switch(direction) {
                case Room.DoorDirection.Left: 
                    newRoomPositionInMap += new Vector2Int(-1, 0);
                    break;
                case Room.DoorDirection.Top: 
                    newRoomPositionInMap += new Vector2Int(0, 1);
                    break;
                case Room.DoorDirection.Right:
                    newRoomPositionInMap += new Vector2Int(1, 0);
                    break;
                case Room.DoorDirection.Bottom: 
                    newRoomPositionInMap += new Vector2Int(0, -1);
                    break;
                default: 
                    throw new System.ArgumentOutOfRangeException(nameof(direction), $"Invalid DoorDirection value: {direction}");
            }

            if(placedRooms.ContainsKey(newRoomPositionInMap)){
                //Skip placing a room in the same spot
                continue;
            } else {
                placedRooms[newRoomPositionInMap] = newRoom;
            }


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
            instantiateRooms(newRoom, newRoomPositionInMap, remainingDeepness - 1);
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