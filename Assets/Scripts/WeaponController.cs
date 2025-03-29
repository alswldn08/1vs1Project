using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon weapon;
    public GameObject bulletPrefab;
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        weapon.InitSetting();
    }



    // Update is called once per frame
    void Update()
    {
        weapon.Shoot(bulletPrefab, firePoint);
    }
}
