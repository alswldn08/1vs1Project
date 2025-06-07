using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCutScene : MonoBehaviour
{
    [Header("컷신 이미지들 (순서대로 넣기)")]
    [SerializeField] private List<Image> cutsceneImages;

    [Header("대화 시작 트리거 오브젝트")]
    [SerializeField] private GameObject talkTrigger;

    private int currentIndex = 0;
    private bool isCutSceneActive = false;

    private void Start()
    {
        // 모든 컷신 이미지 비활성화
        foreach (var image in cutsceneImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isCutSceneActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            ShowNextImage();
        }
    }

    private void ShowNextImage()
    {
        // 이전 이미지 비활성화
        if (currentIndex > 0)
        {
            cutsceneImages[currentIndex - 1].gameObject.SetActive(false);
        }

        // 다음 이미지가 있다면 보여줌
        if (currentIndex < cutsceneImages.Count)
        {
            cutsceneImages[currentIndex].gameObject.SetActive(true);
            currentIndex++;
        }
        else
        {
            EndCutScene();
        }
    }

    private void StartCutScene()
    {
        isCutSceneActive = true;
        currentIndex = 0;
        Time.timeScale = 0f; // 게임 멈춤
        ShowNextImage();     // 첫 이미지 보여줌
    }

    private void EndCutScene()
    {
        isCutSceneActive = false;
        Time.timeScale = 1f; // 게임 재개

        // 마지막 이미지 끄기
        if (cutsceneImages.Count > 0)
            cutsceneImages[cutsceneImages.Count - 1].gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject == talkTrigger)
        {
            StartCutScene();
        }
    }
}
