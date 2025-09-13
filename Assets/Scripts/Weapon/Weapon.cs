using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data // 전략패턴으로 사용될 데이터
{
    public int maxBullet;
    public int currentBullet; // 현재 남은 총알 개수
    public float reloadTime;  // 재장전 시간
    public float coolTime;    // 발사 간격
    public bool isReloading;
}

public abstract class Weapon : MonoBehaviour
{
    public Data data;

    private WeaponUI weaponUI;
    private WeaponController WController;
    private Glock glock;
    private Rifle rifle;

    protected Player player;
    private float lastShotTime = 0f; // 마지막 발사 시간 기록

    public void SetPlayer(Player p)
    {
        player = p;
    }

    public abstract void InitSetting();

    public virtual void Shoot(GameObject bulletPrefab, Transform firePoint)
    {
        if (WController.weapon == glock)
        {
            if (Input.GetMouseButtonDown(1))
            {
                TryShoot(bulletPrefab, firePoint);
            }
        }

        if (WController.weapon == rifle)
        {
            if (Input.GetMouseButton(1))
            {
                TryShoot(bulletPrefab, firePoint);
            }
        }
    }

    private void TryShoot(GameObject bulletPrefab, Transform firePoint)
    {
        // 쿨타임 체크
        if (Time.time < lastShotTime + data.coolTime) return;

        if (data.currentBullet > 0)
        {
            player?.ChangeAnimation(3, 1f);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            float direction = transform.eulerAngles.y == 180 ? -1f : 1f;
            bullet.GetComponent<Bullet>().SetDirection(direction);

            data.currentBullet--;
            lastShotTime = Time.time; // 발사 시간 갱신

            if (weaponUI != null)
            {
                Debug.Log($"[{gameObject.name}] 총알 발사, 남은 탄환: {data.currentBullet}");
                weaponUI.CountBullet(this);
            }
            else
            {
                Debug.LogError("weaponUI가 null 상태임!");
            }
        }
        else
        {
            Debug.Log($"[{gameObject.name}] 탄환 소진됨, WeaponUI 호출 시도");
            if (weaponUI != null)
            {
                weaponUI.WeaponStateText(this, "탄환 소진");
            }
            else
            {
                Debug.LogError("weaponUI가 null 상태임!");
            }
        }
    }

    public virtual IEnumerator Reload()
    {
        if (data.currentBullet == data.maxBullet || data.isReloading) yield break;

        data.isReloading = true;
        Debug.Log($"[{gameObject.name}] 재장전 시작");
        weaponUI?.WeaponStateText(this, "재장전 중...");

        yield return new WaitForSeconds(data.reloadTime);

        data.currentBullet = data.maxBullet;
        Debug.Log($"[{gameObject.name}] 재장전 완료, 탄환 {data.currentBullet}/{data.maxBullet}");
        weaponUI?.WeaponStateText(this, "재장전 완료");
        data.isReloading = false;

        if (weaponUI != null)
        {
            weaponUI.CountBullet(this);
        }
        else
        {
            Debug.LogError("weaponUI가 null 상태임!");
        }
    }

    protected virtual void Start()
    {
        data.currentBullet = data.maxBullet;
        weaponUI = FindObjectOfType<WeaponUI>();
        WController = FindObjectOfType<WeaponController>();
        glock = FindObjectOfType<Glock>();
        rifle = FindObjectOfType<Rifle>();

        if (weaponUI == null)
            Debug.LogError("WeaponUI를 찾지 못했습니다. 씬에 WeaponUI 오브젝트가 있는지 확인하세요.");
    }
}
