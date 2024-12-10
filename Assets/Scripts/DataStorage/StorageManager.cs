using System.IO;
using UnityEngine;

public class StorageManager {
    
    public void clearAll() {
        var unlockables = new UnlockablesStorage();
        unlockables.reset();
    }
}