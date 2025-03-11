using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // 用於控制敵人的顏色透明度
    private EnemyPool pool; // 取得物件池的引用

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 獲取敵人的 SpriteRenderer
        
        pool = EnemyPool.Instance; // 確保 pool 變數指向 EnemyPool
    }
    public void FadeOutAndReturnToPool(float fadeDuration)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>(); // 嘗試重新獲取
            if (spriteRenderer == null)
            {
                Debug.LogError("FadeOutCoroutine: 找不到 SpriteRenderer，無法執行淡出！");
                return;
            }
        }

        if (pool == null)
        {
            pool = EnemyPool.Instance; // 嘗試重新獲取
            if (pool == null)
            {
                Debug.LogError("FadeOutCoroutine: EnemyPool 實例為 null，無法執行淡出！");
                return;
            }
        }

        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        if (spriteRenderer == null || pool == null)
        {
            Debug.LogError("FadeOutCoroutine: spriteRenderer 或 pool 為 null，無法執行淡出！");
            yield break; // 直接結束 Coroutine，避免錯誤
        }

        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;  // 儲存原來的顏色
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // 目標顏色：完全透明

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(originalColor, targetColor, elapsedTime / duration); // 逐漸過渡到透明
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // **等待一小段時間，確保淡出後才回收**
        yield return new WaitForSeconds(0.05f);

        // 讓物件池回收敵人
        pool.ReturnEnemy(gameObject);
    }
}

