using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossenemyMove : MonoBehaviour{

    public float moveSpeed = 5f;            //敵人移動速度
    public float stoppingDistance = 0.5f;   //停止追蹤的距離

    private Transform player;               //玩家Transform

    void Start(){

        //查找玩家獲取Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        if (Time.timeScale != 0){
             
            //檢查敵人與玩家的距離 是否需要繼續移動
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance){
                //計算方向向量
                Vector2 direction = (player.position - transform.position).normalized;

                //移動敵人朝著玩家 (unscaledDeltaTime忽略時間縮放)
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.unscaledDeltaTime);
            }
        }
    }
}
