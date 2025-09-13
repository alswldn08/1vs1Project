using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [Header("WeaponUI")]
    public Text countRifle;
    public Text countGlock;
    public Text stateText;

    private Rifle rifle;
    private Glock glock;

    void Start()
    {
        rifle = FindObjectOfType<Rifle>();
        glock = FindObjectOfType<Glock>();
        InitializeUI();

        if (stateText == null)
            Debug.LogError("stateText가 인스펙터에 연결되지 않았습니다!");
    }

    public void InitializeUI()
    {
        if (rifle != null && countRifle != null)
        {
            countRifle.text = rifle.data.currentBullet + "/" + rifle.data.maxBullet;
        }
        if (glock != null && countGlock != null)
        {
            countGlock.text = glock.data.currentBullet + "/" + glock.data.maxBullet;
        }
    }

    public void UpdateWeapon(Weapon newWeapon)
    {
        CountBullet(newWeapon);
    }

    public void CountBullet(Weapon weapon)
    {
        if (weapon is Rifle && countRifle != null)
        {
            countRifle.text = weapon.data.currentBullet + "/" + weapon.data.maxBullet;
        }
        else if (weapon is Glock && countGlock != null)
        {
            countGlock.text = weapon.data.currentBullet + "/" + weapon.data.maxBullet;
        }
    }

    public void WeaponStateText(Weapon weapon, string nowState)
    {
        Debug.Log($"WeaponUI 호출됨 → {weapon.GetType().Name}: {nowState}");

        if (stateText != null)
        {
            stateText.text = $"{weapon.GetType().Name}: {nowState}";
        }
        else
        {
            Debug.LogError("stateText가 인스펙터에 연결되지 않았습니다!");
        }
    }
}
