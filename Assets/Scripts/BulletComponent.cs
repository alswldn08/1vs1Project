using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [Header("총알 격발")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    [Header("재장전")]
    public int maxBullet = 50;
    public int currentBullet; //현재 남은 총알 개수
    public float reloadTime = 2f;
    public bool isReloading = false;


    void Start()
    {
        currentBullet = maxBullet;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentBullet < maxBullet && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        if (isReloading)
            return;

        if (currentBullet > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            float direction = transform.eulerAngles.y == 180 ? -1f : 1f;
            bullet.GetComponent<Bullet>().SetDirection(direction);

            currentBullet--;
        }
        else
        {
            Debug.Log("재장전 하세요");
        }
    }

    private IEnumerator Reload()
    {
        if (currentBullet == maxBullet || isReloading) yield break;

        isReloading = true;
        Debug.Log("재장전 중...");

        yield return new WaitForSeconds(reloadTime);

        currentBullet = maxBullet;
        Debug.Log("재장전 완료!");

        isReloading = false;
    }
}
