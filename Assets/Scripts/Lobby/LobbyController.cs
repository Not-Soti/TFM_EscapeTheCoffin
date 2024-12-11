using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    public GameObject boneGunStand;
    public GameObject magicWandStand;

    public GameObject openDoor;
    public GameObject closedDoor;

    void Start(){
        openDoor.SetActive(false);
        closedDoor.SetActive(true);
        activateUnlockedWeapons();
    }

    private void activateUnlockedWeapons(){
        var storage = new UnlockablesStorage();

        boneGunStand.SetActive(true);
        magicWandStand.SetActive(storage.getIsMagicWandUnlocked());
    }

    public void setWeaponAsDefault(string name){
        var level = GameObject.Find("LevelController").GetComponent<LevelController>();
        level.setChosenWeapon(name);

        openDoor.SetActive(true);
        closedDoor.SetActive(false);
    }
}