using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Collider weaponCol;

    public Collider weaponCol1;
    public Collider weaponCol2;

    public void WeaponEnable()
    {
        weaponCol.enabled = true;
    }

    public void WeaponDisable()
    {
        weaponCol.enabled = false;
    }

    public void Weapon1Enable()
    {
        weaponCol1.enabled = true;
    }

    public void Weapon1Disable()
    {
        weaponCol1.enabled = false;
    }

    public void Weapon2Enable()
    {
        weaponCol2.enabled = true;
    }

    public void Weapon2Disable()
    {
        weaponCol2.enabled = false;
    }
}