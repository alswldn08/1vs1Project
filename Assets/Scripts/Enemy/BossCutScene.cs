using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCutScene : MonoBehaviour
{
    [Header("�ƽ� �̹����� (������� �ֱ�)")]
    [SerializeField] private List<Image> cutsceneImages;

    [Header("��ȭ ���� Ʈ���� ������Ʈ")]
    [SerializeField] private GameObject talkTrigger;

    private int currentIndex = 0;
    private bool isCutSceneActive = false;

    private void Start()
    {
        // ��� �ƽ� �̹��� ��Ȱ��ȭ
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
        // ���� �̹��� ��Ȱ��ȭ
        if (currentIndex > 0)
        {
            cutsceneImages[currentIndex - 1].gameObject.SetActive(false);
        }

        // ���� �̹����� �ִٸ� ������
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
        Time.timeScale = 0f; // ���� ����
        ShowNextImage();     // ù �̹��� ������
    }

    private void EndCutScene()
    {
        isCutSceneActive = false;
        Time.timeScale = 1f; // ���� �簳

        // ������ �̹��� ����
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
