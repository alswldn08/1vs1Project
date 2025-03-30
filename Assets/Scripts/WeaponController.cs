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
    private Player player;

    void Start()
    {
        weapon.InitSetting();
        rifle = GetComponent<Rifle>();
        glock = GetComponent<Glock>();
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<Player>();

        // UI √ ±‚»≠
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
        Debug.Log($"{weapon.GetType().Name} ¿Â¬¯");

        uiManager.UpdateWeapon(weapon);

        if (player != null)
        {
            player.SetWeapon(weapon);
        }
    }
}
