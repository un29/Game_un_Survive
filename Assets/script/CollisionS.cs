using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollisionS : MonoBehaviour
{
    private Rigidbody2D rb;
    public TextMeshProUGUI timeText;        //時間
    public TextMeshProUGUI scoreText;       //分數

    public TextMeshProUGUI gameOverText;    //gameOver
    public Button TryAgainButton;           //再一次按鈕
    public TextMeshProUGUI nextLevelText;   //nextLevel
    public Button nextlevelButton;          //下一關按鈕

    public int score = 0;                   //分數

    public Image bgover;                    //底圖

    public Image hpBar;                     //血條
    public int maxHP = 100;                 //最大血量
    private int currentHP;                  //當前血量

    public float detectionRadius = 2.0f;    //檢測範圍半徑
    public LayerMask enemyLayer;            //專門檢測敵人的圖層

    public float gameTime = 30f;            //遊戲時間(秒)
    private bool isGameOver = false;        //遊戲暫停

    public GameObject killEffect;           //kill特效
    public GameObject player_hit_effect; 
    public GameObject player_heal_effect;


    void Start(){

        //避免遊戲暫停
        Time.timeScale = 1;

        rb = GetComponent<Rigidbody2D>();
        //初始化血量
        currentHP = maxHP;

        //更新血條顯示
        updateHPBar();
        updateScoreText();

        //隱藏物件
        gameOverText.gameObject.SetActive(false); 
        TryAgainButton.gameObject.SetActive(false);
        nextlevelButton.gameObject.SetActive(false);
        nextLevelText.gameObject.SetActive(false);
        bgover.gameObject.SetActive(false);

        player_hit_effect.gameObject.SetActive(false);
        player_heal_effect.gameObject.SetActive(false);

        //啟動遊戲計時器
        StartCoroutine(GameTimer());    
    }

    void OnTriggerEnter2D(Collider2D collisionInfo){
        //如果遊戲結束 停止一切操作
        if (isGameOver) return;

        //如果撞到enemy
        if (collisionInfo.tag == "enemy"){

            // 搜索附近的敵人 (point,radius,layerMask)
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
            int enemyCount = nearbyEnemies.Length;

            //如果範圍內有多於 1 個敵人
            if (enemyCount > 1){

                //扣10分 扣血12
                score -= 10; 
                ReduceHP(12);

                //銷毀所有範圍內的敵人
                DestroyEnemy(nearbyEnemies); 

            }else{ //如果範圍內只有 1 個敵人

                //扣10分 扣血12
                score += 12; // +12
                bonusHP(2);

                //如果EnemyPool.Instance 為 null
                if (EnemyPool.Instance == null){
                    return;
                }

                //回收敵人
                EnemyPool.Instance.ReturnEnemy(collisionInfo.gameObject); 
                
                //播放特效
                GameObject effect = Instantiate(killEffect, collisionInfo.transform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }
        }

        //撞到bonus
        if (collisionInfo.tag == "bonus"){

            //加22分 回血10
            score += 22;
            bonusHP(10);

            //銷毀bonus
            Destroy(collisionInfo.gameObject);
        }

        //撞到BossEnemy
        if (collisionInfo.tag == "BossEnemy"){

            //加18分 回血18
            score -= 18;
            ReduceHP(18);

            //銷毀該BossEnemy
            Destroy(collisionInfo.gameObject); 
        }

        //更新分數
        updateScoreText();
    }

    void DestroyEnemy(Collider2D[] enemies) {

        //銷毀列表的所有敵人(當範圍內 敵人 > 1 個敵人)
        foreach (Collider2D enemy in enemies){

            //扣5分 扣血3
            score -= 5;
            ReduceHP(3);

            //銷毀敵人
            Destroy(enemy.gameObject);
        }
    }

    //進入協程
    IEnumerator GameTimer(){

        //當前時間
        float elapsedTime = 0f;

        //開始時間
        int startTime = 30;
  
        while (elapsedTime < gameTime){

            //如果遊戲結束 結束計時器
            if (isGameOver) yield break;

            //unscaledDeltaTime忽略時間縮放
            elapsedTime += Time.unscaledDeltaTime;

            //倒數(換成整數)
            int timeCount = startTime - Mathf.FloorToInt(elapsedTime);

            //開始計時
            if (timeText != null){
                timeText.text = "Time:" + timeCount;
            }

            //等待下一幀
            yield return null;
        }
        
        //時間到 檢查分數是否達標
        if (hpBar.fillAmount < 0.5){

            //未達標 顯示Game Over 暫停遊戲
            TriggerGameOver(); 
        }else{

            //達標  顯示nextLevel暫停遊戲
            TriggernextLevelOver();
        }
    }

    //遊戲結束 (血量未達50)
    void TriggerGameOver(){

        //如果遊戲結束
        isGameOver = true;

        //顯示Game Over
        gameOverText.gameObject.SetActive(true);

        //設置文字內容
        gameOverText.text = "Game Over";         
        TryAgainButton.gameObject.SetActive(true);
        bgover.gameObject.SetActive(true);

        //暫停遊戲
        Time.timeScale = 0f;                     
    }

    //進入下個階段 (血量達到50以上)
    void TriggernextLevelOver(){

        //顯示nextLevel
        nextLevelText.gameObject.SetActive(true); 
        nextLevelText.text = "Next Level";
        nextlevelButton.gameObject.SetActive(true);
        bgover.gameObject.SetActive(true);

        //暫停遊戲
        Time.timeScale = 0f;                     
    }

    void ReduceHP(int damage){

        //扣除血量
        currentHP -= damage;

        //更新血條顯示
        updateHPBar();

        //開啟特效hit
        player_hit_effect.gameObject.SetActive(true);

        //延遲0.5秒執行
        Invoke("DisablePlayerHitEffect", 0.5f);

        //遊戲結束 血量歸零 
        if (currentHP <= 0){
            TriggerGameOver(); 
        }
    }

    void bonusHP(int upHP){

        //開啟特效heal
        player_heal_effect.gameObject.SetActive(true);

        //延遲0.5秒
        Invoke("DisablePlayerHealEffect", 0.5f); 

        //確保血量不會超過最大值
        if (currentHP + upHP > maxHP){
            currentHP = maxHP;
        }else{
            currentHP += upHP;
        }

        //更新血量
        updateHPBar();
    }

    //關閉扣血特效
    void DisablePlayerHitEffect(){
        player_hit_effect.gameObject.SetActive(false); 
    }

    //關閉回血特效
    void DisablePlayerHealEffect(){
        player_heal_effect.gameObject.SetActive(false); 
    }

    //血量轉比例
    void updateHPBar(){
        if (hpBar != null){
            hpBar.fillAmount = (float)currentHP / maxHP; 
        }
    }

    //更新分數
    void updateScoreText(){
        if (scoreText != null){
            scoreText.text = "Score:" + score;
        }
    }

    //可視化檢測範圍
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); //(center,radius)
    }
}
