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

    private Rigidbody2D playerRb; // 플레이어의 Rigidbody2D

    private Coroutine randomCoroutine;
    public int randomValue;

    [Header("포탈에 들어가면 비활성화")]
    public GameObject weapon;
    public GameObject sliders;


    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }

        // UI 초기화
        loadingPG.gameObject.SetActive(false);
        loadingSlider.maxValue = 100f;
        loadingSlider.value = 0f;
        loadingSlider.interactable = false;

        // 플레이어 Rigidbody2D 찾기
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>();
        }

        if (playerRb == null)
        {
            Debug.LogError("Player의 Rigidbody2D를 찾지 못했습니다.");
        }
    }

    public void StartLoading()
    {
        if (weapon != null && sliders != null)
        {
            weapon.SetActive(false);
            sliders.SetActive(false);
        }
            loadingPG.gameObject.SetActive(true);

        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero; //로딩 중에는 플레이어 움직임 봉인
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        loadingSlider.value = 0f;
        randomCoroutine = StartCoroutine(RandomValue());
    }

    private void FixedUpdate()
    {
        if (loadingPG.gameObject.activeSelf)
        {
            loadingSlider.value += randomValue * Time.deltaTime;

            if (loadingSlider.value >= loadingSlider.maxValue)
            {
                if (randomCoroutine != null)
                {
                    StopCoroutine(randomCoroutine);
                    if(weapon != null && sliders != null)
                    {
                        weapon.SetActive(true);
                        sliders.SetActive(true);
                    }
                    randomCoroutine = null;
                }

                // 플레이어 움직임 제한 해제
                if (playerRb != null)
                {
                    playerRb.constraints = RigidbodyConstraints2D.None;
                    playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
                }

                string sceneName = SceneManager.GetActiveScene().name;

                // 현재 씬에 따라 다음 씬으로 이동
                switch (sceneName)
                {
                    case "Title":
                        MoveSceneManager.i.MoveScene1();
                        SoundManager.i.PlayBBM(2);
                        break;
                    case "Stage1":
                        MoveSceneManager.i.MoveScene2();
                        SoundManager.i.PlayBBM(3);
                        break;
                    case "Stage2":
                        MoveSceneManager.i.MoveScene3();
                        SoundManager.i.PlayBBM(4);
                        break;
                    case "Stage3":
                        MoveSceneManager.i.MoveScene1();
                        break;
                }
            }
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
