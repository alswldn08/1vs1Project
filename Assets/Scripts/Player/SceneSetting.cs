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
        // 1. ī�޶� ã��
        var vcam = FindObjectOfType<CinemachineVirtualCamera>();
        if (vcam != null)
        {
            vcam.Follow = transform;
        }
        else Debug.LogWarning("CinemachineVirtualCamera�� ã�� ���߽��ϴ�.");

        // 2. ���� ����Ʈ ã��
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
        else Debug.LogWarning("SpawnPoint �±׸� ã�� ���߽��ϴ�.");

        var skillEmission = FindObjectOfType<SkillEmission>();

        if (skillEmission != null)
        {
            skillEmission.target = gameObject.transform;
        }
        else Debug.LogWarning("SkillEmission�� ã�� ���߽��ϴ�.");

        var BossUI = FindObjectOfType<BossHP>();

        if(BossUI != null)
        {
            BossUI.bossUI = bossHpUI;
            BossUI.hpSlider = bossHpSlider;
        }
    }
}
