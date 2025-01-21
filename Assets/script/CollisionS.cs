using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; //

public class CollisionS : MonoBehaviour
{
    private Rigidbody2D rb;
    public TextMeshProUGUI timeText; //時間
    public TextMeshProUGUI scoreText; // 分數
    public TextMeshProUGUI gameOverText; // gameOver
    public Button TryAgainButton;
    public TextMeshProUGUI nextLevelText; // nextLevel
    public Button nextlevelButton;

    public int score = 0;

    public Image bgover; // 底圖

    public Image hpBar; // 血條
    public int maxHP = 100; // 最大血量
    private int currentHP; // 當前血量

    public float detectionRadius = 2.0f; // 檢測範圍半徑
    public LayerMask enemyLayer; // 專門檢測敵人的圖層

    public float gameTime = 30f; // 遊戲時間（秒）
    private bool isGameOver = false;

    public GameObject killEffect; // kill特效
    public GameObject player_hit_effect; 
    public GameObject player_heal_effect;


    void Start(){

        Time.timeScale = 1;

        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;// 初始化血量
        updateHPBar();// 更新血條顯示
        updateScoreText();

        gameOverText.gameObject.SetActive(false); // 隱藏
        TryAgainButton.gameObject.SetActive(false);
        nextlevelButton.gameObject.SetActive(false);
        nextLevelText.gameObject.SetActive(false);
        bgover.gameObject.SetActive(false);

        player_hit_effect.gameObject.SetActive(false);
        player_heal_effect.gameObject.SetActive(false);

        StartCoroutine(GameTimer());    // 啟動遊戲計時器
    }

    void OnTriggerEnter2D(Collider2D collisionInfo){
        if (isGameOver) return; // 如果遊戲結束，停止一切操作

        if (collisionInfo.tag == "enemy"){
            //Debug.Log("撞到enemy");

            // 搜索附近的敵人
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer); //point,radius,layerMask
            int enemyCount = nearbyEnemies.Length;

            if (enemyCount > 1){ // 如果範圍內有多於 1 個敵人

                score -= 10; // 扣 10
                ReduceHP(13); 
                DestroyEnemy(nearbyEnemies); // 銷毀所有範圍內的敵人
                Debug.Log("範圍內有多個敵人，-10");
            }

            else{ // 如果範圍內只有 1 個敵人
                score += 12; // +12
                bonusHP(2); 
                Destroy(collisionInfo.gameObject); // 銷毀該敵人

                // 播放特效
                GameObject effect = Instantiate(killEffect, collisionInfo.transform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }
        }

        if (collisionInfo.tag == "bonus"){ //撞到bonus
            score += 22; // 加22
            bonusHP(9);
            Debug.Log("+22");
            Destroy(collisionInfo.gameObject); // 銷毀該bonus
        }

        if (collisionInfo.tag == "BossEnemy"){ //撞到BOSSenemy
            score -= 18; // -18
            ReduceHP(20);
            Debug.Log("-BOSS 18");
            Destroy(collisionInfo.gameObject); // 銷毀該BOSSenemy
        }

        updateScoreText(); // 更新
    }

    void DestroyEnemy(Collider2D[] enemies) {
        // 銷毀列表中的所有敵人
        foreach (Collider2D enemy in enemies){
            Destroy(enemy.gameObject);
        }
    }

    IEnumerator GameTimer(){
        float elapsedTime = 0f;
        int startTime = 30;
  
        while (elapsedTime < gameTime){
            if (isGameOver) yield break; // 如果遊戲結束，結束計時器
            elapsedTime += Time.unscaledDeltaTime; // 使用 unscaledDeltaTime 來忽略時間縮放

            int timeCount = startTime - Mathf.FloorToInt(elapsedTime); //倒數(換成整數)
            if (timeText != null){
                timeText.text = "Time:" + timeCount;
            }

            yield return null; // 等待下一幀
        }
        
        // 時間到，檢查分數是否達標
        if (hpBar.fillAmount < 0.5){
            TriggerGameOver(); // 顯示 "Game Over" 並暫停遊戲
        }
        else{
            TriggernextLevelOver();
        }
    }

    void TriggerGameOver(){
        isGameOver = true;
        gameOverText.gameObject.SetActive(true); // 顯示 "Game Over"
        gameOverText.text = "Game Over";         // 設置文字內容
        TryAgainButton.gameObject.SetActive(true);
        bgover.gameObject.SetActive(true);
        Time.timeScale = 0f;                     // 暫停遊戲
        Debug.Log("遊戲結束，血量未達50");
    }

    void TriggernextLevelOver(){
        nextLevelText.gameObject.SetActive(true); // 顯示 "nextLevel"
        nextLevelText.text = "Next Level";
        nextlevelButton.gameObject.SetActive(true);
        bgover.gameObject.SetActive(true);
        Time.timeScale = 0f;                     // 暫停遊戲
        Debug.Log("進入下個階段，血量達到50以上");
    }

    void ReduceHP(int damage){
        currentHP -= damage; // 扣除血量
        updateHPBar();       // 更新血條顯示

        player_hit_effect.gameObject.SetActive(true);
        //Debug.Log("開啟特效hit");
        Invoke("DisablePlayerHitEffect", 0.5f); // 延遲0.5秒執行


        if (currentHP <= 0){
            TriggerGameOver(); // 血量歸零，遊戲結束
        }
    }

    void bonusHP(int upHP){

        player_heal_effect.gameObject.SetActive(true);
        //Debug.Log("開啟特效heal");
        Invoke("DisablePlayerHealEffect", 0.5f); // 延遲0.5秒執行

        if (currentHP <= maxHP){
            currentHP += upHP; // 加血量
            updateHPBar();       // 更新血條顯示
        }
    }

    void DisablePlayerHitEffect(){
        player_hit_effect.gameObject.SetActive(false); // 關閉特效
    }
    void DisablePlayerHealEffect(){
        player_heal_effect.gameObject.SetActive(false); // 關閉特效
    }

    void updateHPBar(){
        if (hpBar != null){
            hpBar.fillAmount = (float)currentHP / maxHP; // 將血量轉換為比例
        }
    }
    void updateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score:" + score;
        }
    }

    // 可視化檢測範圍（僅供調試用）
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); //(center,radius)
    }
}
