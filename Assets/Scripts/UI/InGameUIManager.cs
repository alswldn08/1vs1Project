using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        loadingPG.gameObject.SetActive(false);

        loadingSlider.maxValue = 100f;
        loadingSlider.value = 0f;
    }

    public void StartLoading()
    {
        loadingPG.gameObject.SetActive(true);
        StartCoroutine(RandomValue());
    }


    private void FixedUpdate()
    {
        loadingSlider.value += randomValue * Time.deltaTime;

        if (loadingSlider.value == loadingSlider.maxValue)
        {
            string sceneName = SceneManager.GetActiveScene().name;

            switch (sceneName)
            {
                case "Stage1":
                    MoveSceneManager.i.MoveScene2();
                    break;
                case "Stage2":
                    MoveSceneManager.i.MoveScene3();
                    break;
                case "Stage3":
                    MoveSceneManager.i.MoveScene1();
                    break;
            }

            //loadingPG.gameObject.SetActive(false);
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
