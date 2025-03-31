using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour{

    public GameObject enemyPrefab;
    public int poolSize = 10;                                         //物件池大小

    public float Radius = 2.0f;                                       //檢測範圍
    public Color gizmoColor = Color.red;                              //圖示顏色

    private Queue<GameObject> enemyPool = new Queue<GameObject>();    //儲存回收的敵人
    private List<GameObject> activeEnemies = new List<GameObject>();  //追蹤場上的敵人

    public float detectionRadius = 0.5f;                              //檢測範圍半徑
    public LayerMask enemyLayer;                                      //專門檢測敵人的圖層

    public static EnemyPool Instance;                                 //單例模式(方便在其他地方訪問)

    private void Awake(){

        if (Instance == null){

            //EnemyPool實例初始化成功
            Instance = this;

        }else{

            //防止重複實例
            Destroy(gameObject);
        }
        InitializePool();
    }

    //物件池
    private void InitializePool(){

        //Enemy Prefab尚未設置
        if (enemyPrefab == null){
            return;
        }

        //創建敵人
        for (int i = 0; i < poolSize; i++){

            //創建
            GameObject enemy = Instantiate(enemyPrefab);

            //設為不顯示
            enemy.SetActive(false);

            //將敵人加入物件池
            enemyPool.Enqueue(enemy);
        }
    }

    //從物件池中拿敵人
    public GameObject GetEnemy(Vector3 position){

        GameObject enemy;
        if (enemyPool.Count > 0){

            //取出敵人
            enemy = enemyPool.Dequeue(); 

        }else{//物件池中沒敵人+1

            //創建新敵人
            enemy = Instantiate(enemyPrefab); 
        }

        //取出敵人後 重設其透明度
        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null){

            Color color = spriteRenderer.color;

            //設置透明度為完全不透明
            color.a = 1f;  
            spriteRenderer.color = color;
        }

        //設置敵人位置
        enemy.transform.position = position;

        //啟動敵人物件
        enemy.SetActive(true);

        //確保敵人被追蹤
        activeEnemies.Add(enemy); 

        //檢查是否需要回收
        CheckAndRecycleEnemies();

        return enemy;
    }

    //回收敵人
    public void ReturnEnemy(GameObject enemy){

            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
    }

    public void ReturnEnemy2(GameObject enemy){

        //回收null物件
        if (enemy == null){

            return;
        }

        //敵人已經是非活動狀態 忽略回收
        if (!enemy.activeSelf){

            return;
        }

        //活躍列表中移除敵人
        if (activeEnemies.Contains(enemy)){

            activeEnemies.Remove(enemy); 
        }

        //確保物件還是活動狀態才呼叫Coroutine
        deleteEnemy deleteScript = enemy.GetComponent<deleteEnemy>();

        if (deleteScript != null && enemy.activeSelf){

            //淡出時間 //敵人自己處理 Coroutine
            enemy.GetComponent<deleteEnemy>().FadeOutAndReturnToPool(0.15f);  

        }else{

            //直接回收
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    //範圍內回收敵人
    public void CheckAndRecycleEnemies(){

        //候選回收列表
        List<GameObject> potentialRecycleTargets = new List<GameObject>(); 

        for (int i = 0; i < activeEnemies.Count; i++){

            GameObject enemy = activeEnemies[i];

            //防止MissingReference
            if (enemy == null) continue; 

            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(enemy.transform.position, detectionRadius, enemyLayer);

            //當這個範圍內超過4隻
            if (nearbyEnemies.Length > 4) {

                foreach (Collider2D collider in nearbyEnemies){

                    GameObject targetEnemy = collider.gameObject;
                    if (activeEnemies.Contains(targetEnemy) && !potentialRecycleTargets.Contains(targetEnemy)){

                        potentialRecycleTargets.Add(targetEnemy);
                    }
                }
            }
        }

        //隨機選擇 1 隻敵人來回收
        if (potentialRecycleTargets.Count > 0){

            //回收敵人位置
            GameObject enemyToRecycle = potentialRecycleTargets[Random.Range(0, potentialRecycleTargets.Count)];
            ReturnEnemy2(enemyToRecycle);
        }
    }

    //可視化
    private void OnDrawGizmos(){
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
