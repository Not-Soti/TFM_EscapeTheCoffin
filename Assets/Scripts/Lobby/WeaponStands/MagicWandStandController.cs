
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWandStandController : IWeaponStandController {

    public override void setWeaponAsDefault() {
        transform.parent.GetComponent<LobbyController>().setWeaponAsDefault("MagicWand");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        setWeaponAsDefault();
    }
}