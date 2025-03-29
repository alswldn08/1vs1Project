using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{

    protected override void Start()
    {
        InitSetting();  // 먼저 데이터 설정
        base.Start();   // 이후 부모의 Start() 실행

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
