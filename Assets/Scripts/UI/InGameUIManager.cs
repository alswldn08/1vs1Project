using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager i { get; private set; }

    [Header("Slider")]
    public Slider loadingSlider;
    [Header("Image")]
    public Image loadingPG;

    public int randomValue;

    private void Awake()
    {
        if(i == null)
        {
            i = this;
        }


        loadingPG.gameObject.SetActive(true);

        loadingSlider.maxValue = 100f;
        loadingSlider.value = 0f;
    }
    void Start()
    {
        StartCoroutine(RandomValue());
    }


    private void FixedUpdate()
    {
        loadingSlider.value += randomValue * Time.deltaTime;

        if (loadingSlider.value == loadingSlider.maxValue)
        {
            loadingPG.gameObject.SetActive(false);
            StopCoroutine(RandomValue());
        }
    }

    IEnumerator RandomValue()
    {
        while (true)
        {
            randomValue = Random.Range(5, 20);

            yield return new WaitForSeconds(3);
        }
    }
}
