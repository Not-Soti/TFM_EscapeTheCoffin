using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Room {

    public GameObject prefab;

    public Room(GameObject prefab){
        this.prefab = prefab;
    }

    public void setPrefab(GameObject prefab){
        this.prefab = prefab;
    }

    public List<DoorDirection> getAvailableDirections(){
        List<DoorDirection> directions = new List<DoorDirection>();

        if(prefab.transform.Find("DoorLeft") != null){
            directions.Add(DoorDirection.Left);
        }
        if(prefab.transform.Find("DoorTop") != null){
            directions.Add(DoorDirection.Top);
        }
        if(prefab.transform.Find("DoorRight") != null){
            directions.Add(DoorDirection.Right);
        }
        if(prefab.transform.Find("DoorBottom") != null){
            directions.Add(DoorDirection.Bottom);
        }

        return directions;
    }

    public enum DoorDirection {
        Left,
        Top,
        Right,
        Bottom
    }

    public static string GetDoorNameFromDirection(DoorDirection direction){
        switch(direction) {
            case DoorDirection.Left: return "DoorLeft";
            case DoorDirection.Top: return "DoorTop";
            case DoorDirection.Right: return "DoorRight";
            case DoorDirection.Bottom: return "DoorBottom";
            default: 
                throw new ArgumentOutOfRangeException(nameof(direction), $"Invalid DoorDirection value: {direction}");
        }
    }

    public static DoorDirection GetOppositeDirection(DoorDirection direction){
        switch(direction) {
            case DoorDirection.Left: return DoorDirection.Right;
            case DoorDirection.Top: return DoorDirection.Bottom;
            case DoorDirection.Right: return DoorDirection.Left;
            case DoorDirection.Bottom: return DoorDirection.Top;
            default: 
                throw new ArgumentOutOfRangeException(nameof(direction), $"Invalid DoorDirection value: {direction}");
        }
    }

    public override string ToString(){
        string dirs = "\t{\n";
        foreach (Room.DoorDirection newDir in getAvailableDirections()){
            dirs += "\t\t";
            dirs += Room.GetDoorNameFromDirection(newDir);
            dirs += ",\n";
        }
        dirs += "\t}\n";

        string result = "Room { \n";
        result += dirs;
        result += "\n}";
        
        return result;
    }
}

