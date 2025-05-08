using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class PlayerEnergy : MonoBehaviour
{
    public Slider energy;



    void Start()
    {
        energy.maxValue = 100f;
        energy.value = 100f;
    }


    private void FixedUpdate()
    {
        if(energy.value < energy.maxValue)
        {
            energy.value += 1f * Time.deltaTime;
        }
    }
}
