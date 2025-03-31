using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject enemyPrefab;  //敵人預製物
    public int enemyCount = 5;      //每次生產5隻

    private Vector2 Min;            //生成範圍的左下角
    private Vector2 Max;            //生成範圍的右上角

    public Transform player;        //玩家位置
    public float minDistance = 3f;  //與玩家的最小距離

    public float spawnTime= 2f;     //生成間隔時間(秒)

    void Start(){

        //開始定期生成敵人
        InvokeRepeating("SpawnEnemies", 0.5f, spawnTime);

        //更新邊界
        UpdateBoundaries();
    }

    void SpawnEnemies(){

        //已成功生成的敵人數量
        int spawnedEnemies = 0; 

        while (spawnedEnemies < enemyCount){
            Vector2 spawnPosition;

            //嘗試次數
            int attempts = 0;

            //最大嘗試次數限制 避免迴圈
            int maxAttempts = 100;
             
            do{
                //隨機生成敵人位置
                spawnPosition = new Vector2(Random.Range(Min.x, Max.x),
                Random.Range(Min.y, Max.y));

                attempts++;
            }
            //必免離player太近
            while (Vector2.Distance(spawnPosition, player.position) < minDistance && attempts < maxAttempts);

            //物件池EnemyPool中獲取敵人
            if (attempts < maxAttempts){
                
                //從物件池中獲取敵人
                GameObject enemy = EnemyPool.Instance.GetEnemy(spawnPosition);
   
                spawnedEnemies++;

            }else{//無法在安全距離外生成敵人

                break;
            }

            //更新邊界
            UpdateBoundaries();
        }
    }

    //更新邊界
    void UpdateBoundaries(){

        //獲取攝影機的邊界
        Camera cam = Camera.main;

        if (cam != null){

            //攝影機的高度 / 寬度
            float camHeight = 2f * cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            //設置邊界 (攝影機為中心)
            Min = new Vector2((-camWidth / 2) + 0.37f, (-camHeight / 2) + 0.45f);
            Max = new Vector2((camWidth / 2) - 0.38f, (camHeight / 2) - 2.1f);
        }
    }

    //更新邊界 (視窗大小改變)
    void OnRectTransformDimensionsChange(){ //unity自帶

        UpdateBoundaries();
    }
}
