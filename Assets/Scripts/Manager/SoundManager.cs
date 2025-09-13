using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(SetBGMVolum);
            bgmSlider.value = 1;
        }

        if (effectSlider != null)
        {
            effectSlider.onValueChanged.AddListener(SetEffectVolum);
            effectSlider.value = 1;
        }

        PlayBBM(0);
    }

    public void PlayBBM(int Index)
    {
        if (Index < 0 || Index >= bgmList.Count) return;

        bgmSound.clip = bgmList[Index];
        bgmSound.Play();
    }

    public void PlayEffect(int Index)
    {
        if (Index < 0 || Index >= effectList.Count) return;

        // PlayOneShot 사용 → 동시에 여러 사운드 가능
        effectSound.PlayOneShot(effectList[Index]);
    }

    public void PlayPlayerEffect(int Index)
    {
        if (Index < 0 || Index >= PlayerEffectList.Count) return;

        // PlayOneShot 사용 → 동시에 여러 사운드 가능
        playerEffectSound.PlayOneShot(PlayerEffectList[Index]);
    }

    public void SetBGMVolum(float value)
    {
        bgmSound.volume = value;
    }

    public void SetEffectVolum(float value)
    {
        effectSound.volume = value;
        playerEffectSound.volume = value;
    }
}
