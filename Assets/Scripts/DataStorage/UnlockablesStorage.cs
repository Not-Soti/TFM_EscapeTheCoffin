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
        GameData data = new GameData { isMagicWandUnlocked = value };
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(filePath, json);
    }

    public bool getIsMagicWandUnlocked(){
        if(File.Exists(filePath)){
            string json = File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(json);


            return data.isMagicWandUnlocked;
        } else {
            return false;
        }
    }

    public void unlockMagicWand(){
        if(getIsMagicWandUnlocked() == false) {
            setMagicWandUnlocked(true);
        }
    }

    public void reset(){
        setMagicWandUnlocked(false);
    }

}