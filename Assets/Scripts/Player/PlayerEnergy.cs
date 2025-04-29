using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerEnergy : MonoBehaviour
{
    public Slider energy;



    void Start()
    {
        energy.maxValue = 100f;
        energy.value = 100f;
    }


    void Update()
    {
        
    }
}
