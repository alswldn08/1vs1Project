using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSlider : MonoBehaviour
{
    private Player player;

    public Slider hpSlider;
    public Image hpSliderColor; //슬라이더 색상 이미지
    private Color sliderColor;

    void Start()
    {
        player = GetComponent<Player>();    
        
        hpSlider.maxValue = 100f;
        hpSlider.value = 100f;
        hpSlider.interactable = false;

    }


    private void Update()
    {

        if (hpSlider.value < hpSlider.maxValue)
        {
            player.playerHp += 0.5f * Time.deltaTime;
        }

        if (hpSlider.value < 20f)
        {
            sliderColor = Color.red;
        }
        else if(hpSlider.value < 50f)
        {
            sliderColor = Color.yellow;
        }
        else if(hpSlider.value >= 50f)
        {
            sliderColor = Color.green;
        }
        hpSlider.value = player.playerHp;
        hpSliderColor.color = Color.Lerp(hpSliderColor.color, sliderColor, Time.deltaTime * 5f);
    }
}
