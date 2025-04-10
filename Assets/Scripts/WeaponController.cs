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
    private UIManager uiManager;
    private Movement2D moveMent2d;

    void Start()
    {
        weapon.InitSetting();
        rifle = GetComponent<Rifle>();
        glock = GetComponent<Glock>();
        uiManager = FindObjectOfType<UIManager>();
        moveMent2d = FindObjectOfType<Movement2D>();

        // UI �ʱ�ȭ
        uiManager.UpdateWeapon(weapon);
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

        uiManager.UpdateWeapon(weapon);

        if (moveMent2d != null)
        {
            moveMent2d.SetWeapon(weapon);
        }
    }
}
