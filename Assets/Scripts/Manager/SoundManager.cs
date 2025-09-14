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
            SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ���
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� ������ �����̴� �ڵ� ����
        AssignSliders();
    }

    private void AssignSliders()
    {
        // ��Ȱ��ȭ ���� ��� Slider ������Ʈ �˻�
        Slider[] sliders = Resources.FindObjectsOfTypeAll<Slider>();

        foreach (var s in sliders)
        {
            // ���� ���� �����ϴ� ������Ʈ���� Ȯ��
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
