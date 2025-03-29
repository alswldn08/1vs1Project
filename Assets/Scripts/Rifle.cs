using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{

    protected override void Start()
    {
        InitSetting();  // ���� ������ ����
        base.Start();   // ���� �θ��� Start() ����

    }
    public override void InitSetting()
    {
        data.maxBullet = 50;
        data.reloadTime = 2;
        data.isReloading = false;

    }

    public override void Shoot(GameObject bulletPrefab, Transform firePoint)
    {
        base.Shoot(bulletPrefab, firePoint);
    }
}
