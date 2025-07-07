using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [Header("Text")]
    public Text titleText; //타이틀 텍스트
    [Header("ButtonOn")]
    public Button startBtn;
    public Button settingBtn;
    public Button exitBtn;
    public Button helpBtn;
    [Header("ButtonOff")]
    public Button settingOffBtn; //설정창 닫기
    public Button exitYesBtn; //종료창 확인
    public Button exitNoBtn; //종료창 취소
    public Button helpOffBtn; //도움말창 닫기
    [Header("Page")]
    public Image settingPG; //설정 페이지
    public Image exitPG; //나가기 페이지
    public Image helpPG; //도움말 페이지

    void Start()
    {
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

    #region 메인 화면 버튼
    public void StartBtn()
    {
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

    #region 창 내부 버튼
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
}
