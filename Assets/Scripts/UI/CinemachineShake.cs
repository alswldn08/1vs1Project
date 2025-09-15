using UnityEngine;
using Cinemachine;
using System.Collections;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    /// <summary>
    /// 카메라 흔들림 실행
    /// </summary>
    /// <param name="intensity">흔들림 강도</param>
    /// <param name="duration">지속 시간</param>
    public void ShakeCamera(float intensity, float duration)
    {
        if (noise == null) return;
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(intensity, duration));
    }

    private IEnumerator ShakeCoroutine(float intensity, float duration)
    {
        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = 2f; // 좌우 흔들림 속도 (원하는 값으로 조절)

        float elapsed = 0f;
        float startIntensity = intensity;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // 점점 줄어드는 보간 (EaseOut)
            noise.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, t);

            yield return null;
        }

        noise.m_AmplitudeGain = 0f;
    }
}
