using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private BulletComponent bulletComponent;
    public Text countBullet;

    void Start()
    {
        bulletComponent = FindObjectOfType<BulletComponent>();
        CountBullet();

    }
    
    public void CountBullet()
    {
        if(bulletComponent != null && countBullet != null)
        {
            countBullet.text = bulletComponent.currentBullet + "/50";
        }
    }
}
