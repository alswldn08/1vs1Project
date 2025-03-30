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

        // UI �ʱ�ȭ
        uiManager.UpdateWeapon(weapon);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && rifle != null)
        {
            weapon = rifle;
            Debug.Log("������ ����");
            uiManager.UpdateWeapon(weapon); // UI ������Ʈ �߰�
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && glock != null)
        {
            weapon = glock;
            Debug.Log("�۷� ����");
            uiManager.UpdateWeapon(weapon); // UI ������Ʈ �߰�
        }

        weapon.Shoot(bulletPrefab, firePoint);
    }
}
