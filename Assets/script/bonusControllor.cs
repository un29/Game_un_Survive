using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusControllor : MonoBehaviour{
    public GameObject bonusPrefab;// 敵人預製物
    public int bonusCount = 2;
    public Vector2 Min;   // 生成範圍的左下角
    public Vector2 Max;   // 生成範圍的右上角
    public Transform player; // 玩家位置
    public float minDistance = 3f; // 與玩家的最小距離

    public float spawnTime = 10f; // 生成間隔時間（秒）
    public float initialDelay = 5f; // 初始延遲時間（秒)


    void Start(){
        // 開始定期生成
        InvokeRepeating("SpawnBonus", initialDelay, spawnTime);//(生成函式,初始延遲時間,間隔時間)
    }

    void SpawnBonus(){
        int spawnedBonus = 0; // 已成功生成的數量

        while (spawnedBonus < bonusCount)
        {
            Vector2 spawnPosition;
            int attempts = 0;     // 嘗試次數
            int maxAttempts = 100;// 最大嘗試次數限制 避免迴圈

            do{
                // 隨機生成位置
                spawnPosition = new Vector2(Random.Range(Min.x, Max.x),
                Random.Range(Min.y, Max.y));

                attempts++;
            }
            while (Vector2.Distance(spawnPosition, player.position) < minDistance && attempts < maxAttempts);

            if (attempts < maxAttempts){
                Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);// 創建
                //(Prefab,生成的位置,生成旋轉角度"無旋轉")
                spawnedBonus++;
            }
            else{
                Debug.LogWarning("無法在安全距離外生成");
                break;
            }
        }
    }
}
