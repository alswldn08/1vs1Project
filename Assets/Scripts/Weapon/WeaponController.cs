using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon weapon;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rifle rifle;
    private Glock glock;
    private TitleUI titleUI;
    private Player player;

    void Start()
    {
        weapon.InitSetting();
        rifle = GetComponent<Rifle>();
        glock = GetComponent<Glock>();
        titleUI = FindObjectOfType<TitleUI>();
        player = FindObjectOfType<Player>();

        // UI �ʱ�ȭ
        titleUI.UpdateWeapon(weapon);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && rifle != null)
        {
            ChangeWeapon(rifle);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && glock != null)
        {
            ChangeWeapon(glock);
        }

        if (Input.GetKeyDown(KeyCode.R) && weapon.data.currentBullet < weapon.data.maxBullet && !weapon.data.isReloading)
        {
            StartCoroutine(weapon.Reload()); 
        }

        weapon.Shoot(bulletPrefab, firePoint);
    }

    private void ChangeWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
        Debug.Log($"{weapon.GetType().Name}");

        titleUI.UpdateWeapon(weapon);

        if (player != null)
        {
            //moveMent2d.SetWeapon(weapon);
        }
    }
}
