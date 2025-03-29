using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Weapon weapon;
    public Text countRifle;
    public Text countGlock;
    public Data data;

    //public Text countBullet1;

    void Start()
    {
        weapon = FindObjectOfType<Weapon>();
        CountBullet();
    }
    
    public void CountBullet()
    {
        if(weapon != null && countRifle != null)
        {

            countRifle.text = weapon.data.currentBullet + "/" + weapon.data.maxBullet;
        }

        if(weapon != null && countGlock != null)
        {
            countGlock.text = weapon.data.currentBullet + "/" + weapon.data.maxBullet;
        }
    }
}
