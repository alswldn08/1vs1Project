using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [Header("Text")]
    public Text titleText;
    [Header("ButtonOn")]
    public Button startBtn;
    public Button settingBtn;
    public Button exitBtn;
    public Button helpBtn;
    [Header("ButtonOff")]
    public Button settingOffBtn;
    public Button exitYesBtn;
    public Button exitNoBtn;
    public Button helpOffBtn;
    [Header("Page")]
    public Image settingPG;
    public Image exitPG;
    public Image helpPG;
    //[Header("WeaponUI")]
    //public Text countRifle;
    //public Text countGlock;
    //private Rifle rifle;
    //private Glock glock;


    void Start()
    {
        //rifle = FindObjectOfType<Rifle>();
        //glock = FindObjectOfType<Glock>();
        //InitializeUI();

        startBtn.onClick.AddListener(StartBtn);
        settingBtn.onClick.AddListener(SettingBtn);
        exitBtn.onClick.AddListener(ExitBtn);
        helpBtn.onClick.AddListener(HelpBtn);

        settingOffBtn.onClick.AddListener(SettingOffBtn);
        exitYesBtn.onClick.AddListener(ExitYesBtn);
        exitNoBtn.onClick.AddListener(ExitNoBtn);
        helpOffBtn.onClick.AddListener(HelpOffBtn);

        titleText.gameObject.SetActive(true);
        settingPG.gameObject.SetActive(false);
        exitPG.gameObject.SetActive(false);
        helpPG.gameObject.SetActive(false);

    }

    #region ���� ��ư
    public void StartBtn()
    {
        //SceneManager.LoadScene("Stage1");
        LoadingUI.i.StartLoading();
    }
    public void SettingBtn()
    {
        settingPG.gameObject.SetActive(true);
    }
    public void ExitBtn()
    {
        exitPG.gameObject.SetActive(true);
    }
    public void HelpBtn()
    {
        helpPG.gameObject.SetActive(true);

        titleText.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(false);
        settingBtn.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(false);
        helpBtn.gameObject.SetActive(false);
    }
    #endregion

    #region ��� ��ư
    public void SettingOffBtn()
    {
        settingPG.gameObject.SetActive(false);
    }
    public void ExitYesBtn()
    {
        Application.Quit();
    }
    public void ExitNoBtn()
    {
        exitPG.gameObject.SetActive(false);
    }
    public void HelpOffBtn()
    {
        helpPG.gameObject.SetActive(false);

        titleText.gameObject.SetActive(true);
        startBtn.gameObject.SetActive(true);
        settingBtn.gameObject.SetActive(true);
        exitBtn.gameObject.SetActive(true);
        helpBtn.gameObject.SetActive(true);
    }
    #endregion

    #region ����UI
    //public void InitializeUI()
    //{
    //    if (rifle != null && countRifle != null)
    //    {
    //        countRifle.text = rifle.data.currentBullet + "/" + rifle.data.maxBullet;
    //    }
    //    if (glock != null && countGlock != null)
    //    {
    //        countGlock.text = glock.data.currentBullet + "/" + glock.data.maxBullet;
    //    }
    //}

    //public void UpdateWeapon(Weapon newWeapon)
    //{
    //    CountBullet(newWeapon);
    //}

    //public void CountBullet(Weapon weapon)
    //{
    //    if (weapon is Rifle && countRifle != null)
    //    {
    //        countRifle.text = weapon.data.currentBullet + "/" + weapon.data.maxBullet;
    //    }
    //    else if (weapon is Glock && countGlock != null)
    //    {
    //        countGlock.text = weapon.data.currentBullet + "/" + weapon.data.maxBullet;
    //    }
    //}
    #endregion
}
