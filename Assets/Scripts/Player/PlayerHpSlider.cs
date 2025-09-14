using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHpSlider : MonoBehaviour
{
    private Player player;

    public Slider hpSlider;
    public Image[] damageImages; // Image 컴포넌트 배열
    public Text countHp;

    void Start()
    {
        player = GetComponent<Player>();

        // 초기 alpha 0
        foreach (var img in damageImages)
        {
            img.color = new Color(1f, 0f, 0f, 0f);
        }

        hpSlider.maxValue = 100f;
        hpSlider.value = 100f;
        hpSlider.interactable = false;
    }

    private void Update()
    {
        if (hpSlider.value < hpSlider.maxValue)
        {
            player.playerHp += 2f * Time.deltaTime;
        }

        hpSlider.value = player.playerHp;
        countHp.text = hpSlider.value.ToString("0") + "/" + hpSlider.maxValue.ToString("0");
    }

    public IEnumerator Damaged()
    {
        float duration = 0.25f;
        float elapsed = 0f;
        float targetAlpha = 15f / 255f; // 알파 15

        // Fade In
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, targetAlpha, elapsed / duration);
            foreach (var img in damageImages)
                img.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }

        foreach (var img in damageImages)
            img.color = new Color(1f, 0f, 0f, targetAlpha);

        // 잠깐 유지
        yield return new WaitForSeconds(0.1f);

        // Fade Out
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(targetAlpha, 0f, elapsed / duration);
            foreach (var img in damageImages)
                img.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }

        foreach (var img in damageImages)
            img.color = new Color(1f, 0f, 0f, 0f);
    }


    // 다른 스크립트에서 호출
    public void DamagedCoroutine()
    {
        StartCoroutine(Damaged());
    }
}
