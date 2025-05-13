    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class LoadingUI : MonoBehaviour
{
    public static LoadingUI i { get; private set; }

    [Header("Slider")]
    public Slider loadingSlider;
    [Header("Image")]
    public Image loadingPG;

    public int randomValue;

    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }

        loadingPG.gameObject.SetActive(false);

        loadingSlider.maxValue = 100f;
        loadingSlider.value = 0f;
        loadingSlider.interactable = false;
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
                case "Title":
                    SceneManager.LoadScene("Stage1");
                    break;
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
