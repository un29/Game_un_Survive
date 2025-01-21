using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject BossEnemyPrefab;// 敵人預製物
    public int BossEnemyCount = 1;
    private Vector2 Min;   // 生成範圍的左下角
    private Vector2 Max;   // 生成範圍的右上角
    public Transform player; // 玩家位置
    public float minDistance = 3f; // 與玩家的最小距離

    public float spawnTime = 2f; // 生成間隔時間（秒）

    void Start(){
        // 開始定期生成敵人
        InvokeRepeating("SpawnBossEnemies", 0.5f, spawnTime);
    }

    void SpawnBossEnemies(){
        int spawnedEnemies = 0; // 已成功生成的敵人數量

        while (spawnedEnemies < BossEnemyCount)
        {
            Vector2 spawnPosition;
            int attempts = 0;     // 嘗試次數
            int maxAttempts = 100;// 最大嘗試次數限制 避免迴圈

            do{
                // 隨機生成敵人位置
                spawnPosition = new Vector2(Random.Range(Min.x, Max.x),
                Random.Range(Min.y, Max.y));

                attempts++;
                
            }
            while (Vector2.Distance(spawnPosition, player.position) < minDistance && attempts < maxAttempts);

            if (attempts < maxAttempts){
                Instantiate(BossEnemyPrefab, spawnPosition, Quaternion.identity);// 創建敵人
                //(Prefab,生成的位置,生成旋轉角度"無旋轉")
                spawnedEnemies++;
            }
            else{
                Debug.LogWarning("無法在安全距離外生成敵人！");
                break;
            }
        }
    }
    void UpdateBoundaries()
    {
        // 獲取攝影機的邊界
        Camera cam = Camera.main;
        if (cam != null)
        {
            float camHeight = 2f * cam.orthographicSize; // 攝影機的高度
            float camWidth = camHeight * cam.aspect; // 攝影機的寬度

            // 設置邊界
            Min = new Vector2((-camWidth / 2) + 0.37f, (-camHeight / 2) + 0.45f); // 攝影機為中心
            Max = new Vector2((camWidth / 2) - 0.38f, (camHeight / 2) - 2.5f);
        }
    }

    void OnRectTransformDimensionsChange()
    { //unity自帶
        // 更新邊界 (視窗大小改變)
        UpdateBoundaries();
    }
}
