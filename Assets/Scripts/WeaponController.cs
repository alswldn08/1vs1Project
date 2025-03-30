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

    void Start()
    {
        weapon.InitSetting();
        rifle = GetComponent<Rifle>();
        glock = GetComponent<Glock>();
        uiManager = FindObjectOfType<UIManager>();

        // UI 초기화
        uiManager.UpdateWeapon(weapon);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && rifle != null)
        {
            weapon = rifle;
            Debug.Log("라이플 장착");
            uiManager.UpdateWeapon(weapon); // UI 업데이트 추가
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && glock != null)
        {
            weapon = glock;
            Debug.Log("글록 장착");
            uiManager.UpdateWeapon(weapon); // UI 업데이트 추가
        }

        weapon.Shoot(bulletPrefab, firePoint);
    }
}
