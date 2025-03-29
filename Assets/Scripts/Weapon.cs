using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data
{
    public int maxBullet;
    public int currentBullet; //현재 남은 총알 개수
    public float reloadTime;
    public bool isReloading;
}

public abstract class Weapon : MonoBehaviour
{
    public Data data;

    //public GameObject bulletPrefab;
    //public Transform firePoint;
    private UIManager uiManager;

    public abstract void InitSetting();

    public virtual void Shoot(GameObject bulletPrefab, Transform firePoint)
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (data.currentBullet > 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                float direction = transform.eulerAngles.y == 180 ? -1f : 1f;
                bullet.GetComponent<Bullet>().SetDirection(direction);
                data.currentBullet--;
                uiManager.CountBullet();
            }
            else
            {
                Debug.Log("재장전 하세요");
            }
        }
    }

    public virtual IEnumerator Reload()
    {
        if (data.currentBullet == data.maxBullet || data.isReloading) yield break;

        data.isReloading = true;
        Debug.Log("재장전 중...");

        yield return new WaitForSeconds(data.reloadTime);

        data.currentBullet = data.maxBullet;
        Debug.Log("재장전 완료!");

        data.isReloading = false;
        uiManager.CountBullet();
    }

    protected virtual void Start()
    {
        data.currentBullet = data.maxBullet;
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && data.currentBullet < data.maxBullet && !data.isReloading)
        {
            StartCoroutine(Reload());
        }
    }

}
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        Shoot();
    //    }

    //    if (Input.GetKeyDown(KeyCode.R) && data.currentBullet < data.maxBullet && !data.isReloading)
    //    {
    //        StartCoroutine(Reload());
    //    }
    //}

    //void Shoot()
    //{

    //    if (isReloading)
    //        return;

    //    if (currentBullet > 0)
    //    {
    //        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

    //        float direction = transform.eulerAngles.y == 180 ? -1f : 1f;
    //        bullet.GetComponent<Bullet>().SetDirection(direction);
    //        currentBullet--;
    //        uiManager.CountBullet();
    //    }
    //    else
    //    {
    //        Debug.Log("재장전 하세요");
    //    }

    //}

    //private IEnumerator Reload()
    //{
    //    if (currentBullet == maxBullet || isReloading) yield break;

    //    isReloading = true;
    //    Debug.Log("재장전 중...");

    //    yield return new WaitForSeconds(reloadTime);

    //    currentBullet = maxBullet;
    //    Debug.Log("재장전 완료!");

    //    isReloading = false;
    //    uiManager.CountBullet();
    //}
