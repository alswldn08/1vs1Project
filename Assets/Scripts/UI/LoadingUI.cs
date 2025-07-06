using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.EventSystems.EventTrigger;

public class LoadingUI : MonoBehaviour
{
    public static LoadingUI i { get; private set; }

    [Header("Slider")]
    public Slider loadingSlider;
    [Header("Image")]
    public Image loadingPG;

    private Rigidbody2D playerRb; // ���߿� �Ҵ�

    private Coroutine randomCoroutine;
    public int randomValue;

    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }

        // UI �ʱ�ȭ
        loadingPG.gameObject.SetActive(false);
        loadingSlider.maxValue = 100f;
        loadingSlider.value = 0f;
        loadingSlider.interactable = false;

        // Player�� Rigidbody2D ã��
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>();
        }

        if (playerRb == null)
        {
            Debug.LogError("Player�� Rigidbody2D�� ã�� �� �����ϴ�.");
        }
    }

    public void StartLoading()
    {
        loadingPG.gameObject.SetActive(true);

        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero;
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
                    randomCoroutine = null;
                }

                // �÷��̾� �ٽ� ������ �� �ֵ��� Ǯ��
                if (playerRb != null)
                {
                    playerRb.constraints = RigidbodyConstraints2D.None; // ��� ����
                    playerRb.constraints = RigidbodyConstraints2D.FreezeRotation; // ȸ���� ����
                }

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
