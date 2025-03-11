using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int poolSize = 10; // 物件池大小

    public float Radius = 2.0f; // 檢測範圍
    public Color gizmoColor = Color.red; // 圖示顏色

    private Queue<GameObject> enemyPool = new Queue<GameObject>(); // 儲存回收的敵人
    private List<GameObject> activeEnemies = new List<GameObject>(); // 追蹤場上的敵人

    public float detectionRadius = 0.5f; // 檢測範圍半徑
    public LayerMask enemyLayer; // 專門檢測敵人的圖層

    public static EnemyPool Instance; // 單例模式，方便在其他地方訪問

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("EnemyPool 實例初始化成功");
        }
        else
        {
            Destroy(gameObject);// 防止重複實例
        }
        InitializePool();
    }

    private void InitializePool()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab 尚未設置！");
            return;
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab); // 創建敵人
            enemy.SetActive(false);  // 設為不顯示
            enemyPool.Enqueue(enemy); // 將敵人加入物件池
        }
    }

    public GameObject GetEnemy(Vector3 position)
    {

        GameObject enemy;
        if (enemyPool.Count > 0)
        {
            enemy = enemyPool.Dequeue(); // 取出敵人
        }
        else
        {
            Debug.Log("物件池中沒敵人+1");
            enemy = Instantiate(enemyPrefab); // 創建新敵人
        }

        // 取出敵人後，重設其透明度，確保其完全不透明
        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 1f;  // 設置透明度為完全不透明
            spriteRenderer.color = color;
        }

        enemy.transform.position = position; // 設置敵人位置
        enemy.SetActive(true); // 啟動敵人物件

        activeEnemies.Add(enemy); // 確保敵人被追蹤

        // **新增後檢查是否需要回收**
        CheckAndRecycleEnemies();

        return enemy;
    }

        public void ReturnEnemy(GameObject enemy)
    {
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
    }

    public void ReturnEnemy2(GameObject enemy)
    {
        if (enemy == null)
        {
            Debug.LogError("ReturnEnemy2: 企圖回收 null 物件！");
            return;
        }

        if (!enemy.activeSelf)
        {
            Debug.LogWarning("ReturnEnemy2: 敵人已經是非活動狀態，忽略回收。");
            return;
        }

        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy); // 從活躍列表中移除敵人
        }

        // 確保物件還是 active 才呼叫 Coroutine
        deleteEnemy deleteScript = enemy.GetComponent<deleteEnemy>();
        if (deleteScript != null && enemy.activeSelf)
        {
            enemy.GetComponent<deleteEnemy>().FadeOutAndReturnToPool(0.15f);  // 淡出時間 // 敵人自己處理 Coroutine
        }
        else
        {
            // 直接回收
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    public void CheckAndRecycleEnemies()
    {
        List<GameObject> potentialRecycleTargets = new List<GameObject>(); // 候選回收列表

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            GameObject enemy = activeEnemies[i];

            if (enemy == null) continue; // 防止 MissingReferenceException

            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(enemy.transform.position, detectionRadius, enemyLayer);

            if (nearbyEnemies.Length > 4) // **當這個範圍內超過 4 隻**
            {
                foreach (Collider2D collider in nearbyEnemies)
                {
                    GameObject targetEnemy = collider.gameObject;
                    if (activeEnemies.Contains(targetEnemy) && !potentialRecycleTargets.Contains(targetEnemy))
                    {
                        potentialRecycleTargets.Add(targetEnemy);
                    }
                }
            }
        }

        if (potentialRecycleTargets.Count > 0)
        {
            // **隨機選擇 1 隻敵人來回收**
            GameObject enemyToRecycle = potentialRecycleTargets[Random.Range(0, potentialRecycleTargets.Count)];
            Debug.Log($"[CheckAndRecycleEnemies] 回收敵人: {enemyToRecycle.name}，位置: {enemyToRecycle.transform.position}");
            ReturnEnemy2(enemyToRecycle);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
