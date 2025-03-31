using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;  //控制敵人顏色透明度
    private EnemyPool pool;                 //取得物件池的引用

    private void Start(){

        spriteRenderer = GetComponent<SpriteRenderer>();

        //確保pool變數指向EnemyPool
        pool = EnemyPool.Instance; 
    }

    //淡出
    public void FadeOutAndReturnToPool(float fadeDuration){

        if (spriteRenderer == null){

            //嘗試重新獲取
            spriteRenderer = GetComponent<SpriteRenderer>();

            //找不到 SpriteRenderer
            if (spriteRenderer == null){
                return;
            }
        }

        if (pool == null){

            //嘗試重新獲取
            pool = EnemyPool.Instance;

            //EnemyPool 實例為 null
            if (pool == null){
                return;
            }
        }

        //開始執行協程
        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    //協程
    private IEnumerator FadeOutCoroutine(float duration)
    {
        //spriteRenderer或pool為null
        if (spriteRenderer == null || pool == null){

            //直接結束Coroutine避免錯誤
            yield break; 
        }

        float elapsedTime = 0f;

        //儲存原來的顏色 
        Color originalColor = spriteRenderer.color;

        //目標顏色 (完全透明)
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); 

        while (elapsedTime < duration){

            elapsedTime += Time.deltaTime;

            //逐漸過渡到透明
            spriteRenderer.color = Color.Lerp(originalColor, targetColor, elapsedTime / duration); 
            yield return null;
        }

        //spriteRenderer改透明
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        //等待一小段時間 確保淡出後才回收
        yield return new WaitForSeconds(0.05f);

        //讓物件池回收敵人
        pool.ReturnEnemy(gameObject);
    }
}

