using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public float moveSpeed = 3f; // 敵人移動速度
    public float stoppingDistance = 0.5f; // 停止追蹤的距離
    private Transform player; // 玩家物件的 Transform

    void Start(){
        // 查找玩家物件並獲取其 Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        if (Time.timeScale != 0)
        {
            // 檢查敵人與玩家的距離.是否需要繼續移動
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                // 計算方向向量
                Vector2 direction = (player.position - transform.position).normalized;

                // 移動敵人朝著玩家
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.unscaledDeltaTime);// 使用 unscaledDeltaTime 來忽略時間縮放

            }
        }
    }

}
