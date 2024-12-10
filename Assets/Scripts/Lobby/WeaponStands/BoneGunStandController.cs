
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneGunStandController : IWeaponStandController {

    public override void setWeaponAsDefault() {
        transform.parent.GetComponent<LobbyController>().setWeaponAsDefault("BoneGun");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        setWeaponAsDefault();
    }
}