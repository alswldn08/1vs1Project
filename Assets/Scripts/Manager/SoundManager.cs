using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSound;
    public AudioSource effectSound;

    public Slider bgmSlider;
    public Slider effectSlider;

    public List<AudioClip> bgmList;
    public List<AudioClip> effectList;
    public static SoundManager i { get; private set; }

    private void Awake()
    {
        if(i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if(bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(SetBGMVolum);
            bgmSlider.value = 1;
        }
        if(effectSlider != null)
        {
            effectSlider.onValueChanged.AddListener(SetEffectVolum);
            effectSlider.value = 1;
        }
    }


    public void PlayeBBM(int Index)
    {
        bgmSound.clip = bgmList[Index];
    }
    public void PlayeEffect(int Index)
    {
        effectSound.clip = effectList[Index];
    }

    public void SetBGMVolum(float value)
    {
        bgmSound.volume = value;
    }

    public void SetEffectVolum(float value)
    {
        effectSound.volume = value;
    }
}
