using System.Collections;
using UnityEngine;

public class ShakeSystem : MonoBehaviour
{
    public static ShakeSystem Instance { get; private set; }

    private Vector3 originalPos;
    private Coroutine currentShakeCoroutine;

    void Awake()
    {
        if (Instance == null) Instance = this;
        originalPos = transform.position;
    }

    // duration: 흔드는 시간(초), magnitude: 흔드는 강도
    public void Shake(float duration, float magnitude)
    {
        // 이미 흔들리고 있다면 멈추고 위치를 초기화합니다. (연타 시 문제 방지)
        if (currentShakeCoroutine != null)
        {
            StopCoroutine(currentShakeCoroutine);
            transform.position = originalPos;
        }
        currentShakeCoroutine = StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 흔들기가 끝나면 반드시 원래 위치로 복귀
        transform.position = originalPos;
        currentShakeCoroutine = null;
    }
}
