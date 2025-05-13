using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class PlayerEnergySlider : MonoBehaviour
{
    public Slider energy;
    public Image fillColor;
    private Color targetColor;


    void Start()
    {
        energy.maxValue = 100f;
        energy.value = 100f;
        energy.interactable = false;
    }


    private void Update()
    {
        if(energy.value < energy.maxValue)
        {
            energy.value += 10f * Time.deltaTime;
        }

        if (energy.value < 20f)
        {
            targetColor = Color.red;
        }
        else
        {
            targetColor = new Color(1f, 0.6f, 0.2f);
        }
        fillColor.color = Color.Lerp(fillColor.color, targetColor, Time.deltaTime * 5f);
    }
}
