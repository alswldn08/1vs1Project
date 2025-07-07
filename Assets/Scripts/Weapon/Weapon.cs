using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data //전략패턴으로 사용될 데이터
{
    public int maxBullet;
    public int currentBullet; //현재 남은 총알 개수
    public float reloadTime; //재장전 시간
    public float coolTime; 
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

    public void SetPlayer(Player p)
    {
        player = p;
    }

    public abstract void InitSetting();

    public virtual void Shoot(GameObject bulletPrefab, Transform firePoint)
    {
        if(WController.weapon == glock)
        {
            if (Input.GetMouseButtonDown(1))
            {

                if (data.currentBullet > 0)
                {
                    player?.ChangeAnimation(3, 1f);
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                    float direction = transform.eulerAngles.y == 180 ? -1f : 1f;
                    bullet.GetComponent<Bullet>().SetDirection(direction);
                    data.currentBullet--;

                    if (weaponUI != null)
                    {
                        weaponUI.CountBullet(this);
                    }
                    else
                    {
                        Debug.LogError("UIManager가 할당되지 않음");
                    }
                }
                else
                {
                    Debug.Log("재장전 하세요");
                }
            }
        }

        if(WController.weapon == rifle)
        {
            if (Input.GetMouseButton(1))
            {

                if (data.currentBullet > 0)
                {
                    player?.ChangeAnimation(3, 1f);
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                    float direction = transform.eulerAngles.y == 180 ? -1f : 1f;
                    bullet.GetComponent<Bullet>().SetDirection(direction);
                    data.currentBullet--;

                    if (weaponUI != null)
                    {
                        weaponUI.CountBullet(this);
                    }
                    else
                    {
                        Debug.LogError("UIManager가 할당되지 않음");
                    }
                }
                else
                {
                    Debug.Log("재장전 하세요");
                }
            }
        }
    }

    public virtual IEnumerator Reload()
    {
        if (data.currentBullet == data.maxBullet || data.isReloading) yield break;

        data.isReloading = true;
        Debug.Log($"{gameObject.name}: 재장전 중...");

        yield return new WaitForSeconds(data.reloadTime);

        data.currentBullet = data.maxBullet;
        Debug.Log($"{gameObject.name}: 재장전 완료!");

        data.isReloading = false;

        if (weaponUI != null)
        {
            weaponUI.CountBullet(this);
        }
        else
        {
            Debug.LogError("UIManager가 할당되지 않음");
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
        {
            Debug.LogError("UIManager를 찾을 수 없습니다.");
        }
    }
}
