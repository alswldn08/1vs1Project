using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSlider : MonoBehaviour
{
    public Slider hpSlider;
    public Image hpSliderColor;
    private Color sliderColor;

    private Player player;

    private void Awake()
    {

        player = GetComponent<Player>();        
    }
    void Start()
    {
        hpSlider.maxValue = 100f;
        hpSlider.value = 100f;
        hpSlider.interactable = false;

    }


    private void Update()
    {
        if (hpSlider.value < hpSlider.maxValue)
        {
            hpSlider.value += 10f * Time.deltaTime;
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
        hpSlider.value = player.playerHP;
        hpSliderColor.color = Color.Lerp(hpSliderColor.color, sliderColor, Time.deltaTime * 5f);
    }
}
