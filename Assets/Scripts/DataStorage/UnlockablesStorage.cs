using System.IO;
using UnityEngine;

public class UnlockablesStorage {

    private string filePath = Path.Combine(Application.persistentDataPath, "unlockables.json");

    // Clase para almacenar los datos
    [System.Serializable]
    private class GameData{
        public bool isMagicWandUnlocked;
    }

    public void setMagicWandUnlocked(bool value){
        Debug.Log("STM - setMagicWandUnlocked - Enter");
        GameData data = new GameData { isMagicWandUnlocked = value };
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(filePath, json);
        Debug.Log("STM - setMagicWandUnlocked - Exit");
    }

    public bool getIsMagicWandUnlocked(){
        Debug.LogFormat("STM - {0}", Application.persistentDataPath);
        Debug.Log("STM - getMagicWandUnlocked - Enter");
        if(File.Exists(filePath)){
            string json = File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(json);


            Debug.LogFormat("STM - getMagicWandUnlocked - exists = {0} ", data.isMagicWandUnlocked);
            return data.isMagicWandUnlocked;
        } else {
            Debug.Log("STM - getMagicWandUnlocked - Not exists");
            return false;
        }
    }

    public void unlockMagicWand(){
        Debug.Log("STM - unlockMagicWand - Enter");
        if(getIsMagicWandUnlocked() == false) {
            setMagicWandUnlocked(true);
            Debug.Log("STM - unlockMagicWand - created");
        }
    }

    public void reset(){
        setMagicWandUnlocked(false);
    }

}