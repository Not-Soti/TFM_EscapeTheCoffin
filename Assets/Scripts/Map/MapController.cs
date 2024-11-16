using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapController : MonoBehaviour
{

    public List<GameObject> availableRoomPrefabs; //Room prefabs used to build the level
    public int roomGeneratingDeepness;
    public string roomPrefabResourceFolder;

    private List<Room> availableRooms;

    // Stores the map as a matrix, storing the relative coordinates of the matrix where
    // a room is and its value.
    private Dictionary<Vector2Int, Room> placedRooms;

    // Start is called before the first frame update
    void Start()
    {
        if(roomPrefabResourceFolder == null){
            throw new System.Exception("Defining roomPrefabResourceFolder is mandatory");
        }

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
       // placeFinishLevelBeacon();
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
            Room newRoom = getMatchingRoom(direction, remainingDeepness);   

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
    private Room getMatchingRoom(Room.DoorDirection entryDirection, int remainingDeepness) {
        //If last round of doors is being generated, don't let empty doors outside the map
        if(remainingDeepness == 1) {
            /*
            string resourcesPath = string.Format("Levels/{0}/Prefabs/", roomPrefabResourceFolder);
            string roomPrefabPath = null;

            switch(entryDirection) {
            case Room.DoorDirection.Left: 
                roomPrefabPath = resourcesPath + "RoomR";
                break;
            case Room.DoorDirection.Top: 
                roomPrefabPath = resourcesPath + "RoomB";
                break;
            case Room.DoorDirection.Right: 
                roomPrefabPath = resourcesPath + "RoomL";
                break;
            case Room.DoorDirection.Bottom: 
                roomPrefabPath = resourcesPath +  "RoomT";
                break;
            default: 
                throw new System.ArgumentOutOfRangeException(nameof(entryDirection), $"Invalid DoorDirection value: {entryDirection}");
            }

            if(roomPrefabPath != null){
                Debug.LogFormat("STM - roomPrefabPath != null - {0}", roomPrefabPath);
                GameObject matchingRoomPrefab = Instantiate(Resources.Load(roomPrefabPath)) as GameObject;
                Debug.LogFormat("STM - matchingRoomPrefab - gotten");
                if(matchingRoomPrefab != null){
                    Room matchingRoom = new Room(matchingRoomPrefab);
                    Debug.LogFormat("STM - Returning room {0}", matchingRoom.ToString());
                    return matchingRoom;
                } else {
                    Debug.LogFormat("STM - matchingRoomPrefab == null for prefab");
                    throw new System.Exception(string.Format("Couldn't find prefab in {0}", roomPrefabPath));
                }
            } else {
                Debug.LogFormat("STM - roomPrefabPath == null");
                throw new System.Exception();
            }
            */

            GameObject prefab = null;
            switch(entryDirection) {
            case Room.DoorDirection.Left: 
                prefab = availableRoomPrefabs.FirstOrDefault(p => p.name == "RoomR");
                break;
            case Room.DoorDirection.Top: 
                prefab = availableRoomPrefabs.FirstOrDefault(p => p.name == "RoomB");
                break;
            case Room.DoorDirection.Right: 
                prefab = availableRoomPrefabs.FirstOrDefault(p => p.name == "RoomL");
                break;
            case Room.DoorDirection.Bottom: 
                prefab = availableRoomPrefabs.FirstOrDefault(p => p.name == "RoomT");
                break;
            default: 
                throw new System.ArgumentOutOfRangeException(nameof(entryDirection), $"Invalid DoorDirection value: {entryDirection}");
            }

            if(prefab != null){
                return new Room(prefab);
            } else {
                throw new System.Exception(string.Format("Couldnt find prefab for direction {0}", entryDirection));
            }
            
        } else {

            List<Room> matchingRooms = availableRooms.Where(room => room.getAvailableDirections().Contains(Room.GetOppositeDirection(entryDirection))).ToList();
            return matchingRooms.ElementAt(Random.Range(0, matchingRooms.Count));
        }
    }
}