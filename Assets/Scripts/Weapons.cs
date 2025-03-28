using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainWeapon
{
    glock,
    rifle,
    shotgun
}
public enum SubWeapon
{
    grenade,
    knife,
    smoke_grenade
}
public class Weapons : MonoBehaviour
{
    public MainWeapon mainWeapon;
    public SubWeapon subWeapon;
    void Start()
    {
        
    }
}
