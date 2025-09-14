using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSound;
    public AudioSource effectSound;
    public AudioSource playerEffectSound;

    public Slider bgmSlider;
    public Slider effectSlider;

    public List<AudioClip> bgmList;
    public List<AudioClip> effectList;
    public List<AudioClip> PlayerEffectList;

    public static SoundManager i { get; private set; }

    private void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 등록
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때마다 슬라이더 자동 연결
        AssignSliders();
    }

    private void AssignSliders()
    {
        // 비활성화 포함 모든 Slider 오브젝트 검색
        Slider[] sliders = Resources.FindObjectsOfTypeAll<Slider>();

        foreach (var s in sliders)
        {
            // 현재 씬에 존재하는 오브젝트인지 확인
            if (!s.gameObject.scene.IsValid()) continue;

            if (s.CompareTag("BGMSlider"))
            {
                bgmSlider = s;
                bgmSlider.onValueChanged.RemoveAllListeners();
                bgmSlider.onValueChanged.AddListener(SetBGMVolum);
                bgmSlider.value = bgmSound != null ? bgmSound.volume : 1f;
            }
            else if (s.CompareTag("EffectSlider"))
            {
                effectSlider = s;
                effectSlider.onValueChanged.RemoveAllListeners();
                effectSlider.onValueChanged.AddListener(SetEffectVolum);
                effectSlider.value = effectSound != null ? effectSound.volume : 1f;
            }
        }
    }

    private void Start()
    {
        PlayBGM(0);
    }

    public void PlayBGM(int index)
    {
        if (index < 0 || index >= bgmList.Count) return;

        bgmSound.clip = bgmList[index];
        bgmSound.Play();
    }

    public void PlayEffect(int index)
    {
        if (index < 0 || index >= effectList.Count) return;

        effectSound.PlayOneShot(effectList[index]);
    }

    public void PlayPlayerEffect(int index)
    {
        if (index < 0 || index >= PlayerEffectList.Count) return;

        playerEffectSound.PlayOneShot(PlayerEffectList[index]);
    }

    public void SetBGMVolum(float value)
    {
        if (bgmSound != null) bgmSound.volume = value;
    }

    public void SetEffectVolum(float value)
    {
        if (effectSound != null) effectSound.volume = value;
        if (playerEffectSound != null) playerEffectSound.volume = value;
    }
}
