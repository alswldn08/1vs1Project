using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Button")]
    public Button startBtn;
    public Button settingBtn;
    public Button exitBtn;
    public Button helpBtn;


    [Header("WeaponUI")]
    public Text countRifle;
    public Text countGlock;
    private Rifle rifle;
    private Glock glock;

    void Start()
    {
        rifle = FindObjectOfType<Rifle>();
        glock = FindObjectOfType<Glock>();

        startBtn.onClick.AddListener(StartBtn);
        settingBtn.onClick.AddListener(SettingBtn);
        exitBtn.onClick.AddListener(ExitBtn);
        helpBtn.onClick.AddListener(HelpBtn);

        InitializeUI();
    }

    public void StartBtn()
    {

    }
    public void SettingBtn()
    {

    }
    public void ExitBtn()
    {

    }
    public void HelpBtn()
    {

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
}
