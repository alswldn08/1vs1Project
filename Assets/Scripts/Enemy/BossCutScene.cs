using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCutScene : MonoBehaviour //아직 적용안함
{
    [Header("컷씬 이미지들")]
    [SerializeField] private List<Image> cutsceneImages;

    [Header("컷씬 트리거 오브젝트")]
    [SerializeField] private GameObject talkTrigger;

    private int currentIndex = 0;
    private bool isCutSceneActive = false;

    private void Start()
    {
        // 시작 시 모든 컷씬 이미지 비활성화
        foreach (var image in cutsceneImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isCutSceneActive) return;

        // 마우스 클릭 시 다음 컷씬 이미지 표시
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextImage();
        }
    }

    private void ShowNextImage()
    {
        // 이전 이미지 숨기기
        if (currentIndex > 0)
        {
            cutsceneImages[currentIndex - 1].gameObject.SetActive(false);
        }

        // 다음 이미지 표시 또는 컷씬 종료
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
        Time.timeScale = 0f; // 게임 일시정지
        ShowNextImage();     // 첫 이미지 표시
    }

    private void EndCutScene()
    {
        isCutSceneActive = false;
        Time.timeScale = 1f; // 게임 재개

        // 마지막 컷씬 이미지 비활성화
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
