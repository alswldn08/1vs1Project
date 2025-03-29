using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Weapon weapon;
    public Text countBullet;
    public Data data;

    //public Text countBullet1;

    void Start()
    {
        weapon = FindObjectOfType<Weapon>();
        CountBullet();
    }
    
    public void CountBullet()
    {
        if(weapon != null && countBullet != null)
        {
            
            countBullet.text = weapon.data.currentBullet + "/" + weapon.data.maxBullet;
            //countBullet1.text = bulletComponent.currentBullet + "/7";
        }
    }
}
