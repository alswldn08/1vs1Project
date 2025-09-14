using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingUI : MonoBehaviour
{
    public static LoadingUI i { get; private set; }

    [Header("Slider")]
    public Slider loadingSlider;
    [Header("Image")]
    public Image loadingPG;

    private Player player; // Player 스크립트 참조
    private SceneSetting sceneSetting;

    private Coroutine randomCoroutine;
    public int randomValue;

    private void Awake()
    {
        if (i == null) i = this;

        // UI 초기화
        loadingPG.gameObject.SetActive(false);
        loadingSlider.maxValue = 100f;
        loadingSlider.value = 0f;
        loadingSlider.interactable = false;

        player = FindObjectOfType<Player>();
        sceneSetting = FindObjectOfType<SceneSetting>();

        if (player == null)
        {
            Debug.LogError("Player 스크립트를 찾지 못했습니다.");
        }

        // 씬 로드 완료 후 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // 이벤트 해제 (메모리 누수 방지)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void StartLoading()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Stage3 → EndingScene은 로딩창 없이 바로 이동
        if (sceneName == "Stage3")
        {
            player?.MoveOn(); // 플레이어 이동 제한 해제
            Destroy(player.gameObject);
            MoveSceneManager.i.MoveScene4(); // EndingScene으로 이동
            return;
        }

        if (sceneSetting != null)
        {
            sceneSetting.OffInterface();
        }

        loadingPG.gameObject.SetActive(true);

        // Player 움직임 제한
        player?.MoveOff();

        loadingSlider.value = 0f;
        randomCoroutine = StartCoroutine(RandomValue());
    }

    private void FixedUpdate()
    {
        if (!loadingPG.gameObject.activeSelf) return;

        loadingSlider.value += randomValue * Time.deltaTime;

        if (loadingSlider.value >= loadingSlider.maxValue)
        {
            if (randomCoroutine != null)
            {
                StopCoroutine(randomCoroutine);
                randomCoroutine = null;
            }

            // Player 움직임 해제
            player?.MoveOn();

            string sceneName = SceneManager.GetActiveScene().name;

            // 현재 씬에 따라 다음 씬 이동
            switch (sceneName)
            {
                case "Title":
                    MoveSceneManager.i.MoveScene1();
                    break;
                case "Stage1":
                    MoveSceneManager.i.MoveScene2();
                    break;
                case "Stage2":
                    MoveSceneManager.i.MoveScene3();
                    break;
                case "Stage3":
                    MoveSceneManager.i.MoveScene4(); // EndingScene으로 바로 이동
                    break;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 완전히 로드된 후 인터페이스 켜기
        if (sceneSetting != null)
        {
            sceneSetting.OnInterface();
        }

        // 로딩 UI 숨기기
        loadingPG.gameObject.SetActive(false);
        loadingSlider.value = 0f;
    }

    IEnumerator RandomValue()
    {
        while (true)
        {
            randomValue = Random.Range(10, 25);
            yield return new WaitForSeconds(2f);
        }
    }
}
