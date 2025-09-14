using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;

public class SceneSetting : MonoBehaviour
{
    public GameObject playerInterface;
    public GameObject bossHpUI;
    public Slider bossHpSlider;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindCameraAndSpawn();
    }

    public void OnInterface()
    {
        playerInterface.SetActive(true);
    }

    public void OffInterface()
    {
        playerInterface.SetActive(false);
    }

    private void FindCameraAndSpawn()
    {
        // 1. 카메라 찾기
        var vcam = FindObjectOfType<CinemachineVirtualCamera>();
        if (vcam != null)
        {
            vcam.Follow = transform;
        }
        else Debug.LogWarning("CinemachineVirtualCamera를 찾지 못했습니다.");

        // 2. 스폰 포인트 찾기
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
        else Debug.LogWarning("SpawnPoint 태그를 찾지 못했습니다.");

        var skillEmission = FindObjectOfType<SkillEmission>();

        if (skillEmission != null)
        {
            skillEmission.target = gameObject.transform;
        }
        else Debug.LogWarning("SkillEmission을 찾지 못했습니다.");

        var BossUI = FindObjectOfType<BossHP>();

        if(BossUI != null)
        {
            BossUI.bossUI = bossHpUI;
            BossUI.hpSlider = bossHpSlider;
        }
    }
}
