using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int poolSize = 10; // ������j�p

    public float Radius = 2.0f; // �˴��d��
    public Color gizmoColor = Color.red; // �ϥ��C��

    private Queue<GameObject> enemyPool = new Queue<GameObject>(); // �x�s�^�����ĤH
    private List<GameObject> activeEnemies = new List<GameObject>(); // �l�ܳ��W���ĤH

    public float detectionRadius = 0.5f; // �˴��d��b�|
    public LayerMask enemyLayer; // �M���˴��ĤH���ϼh

    public static EnemyPool Instance; // ��ҼҦ��A��K�b��L�a��X��

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("EnemyPool ��Ҫ�l�Ʀ��\");
        }
        else
        {
            Destroy(gameObject);// ����ƹ��
        }
        InitializePool();
    }

    private void InitializePool()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab �|���]�m�I");
            return;
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab); // �ЫؼĤH
            enemy.SetActive(false);  // �]�������
            enemyPool.Enqueue(enemy); // �N�ĤH�[�J�����
        }
    }

    public GameObject GetEnemy(Vector3 position)
    {

        GameObject enemy;
        if (enemyPool.Count > 0)
        {
            enemy = enemyPool.Dequeue(); // ���X�ĤH
        }
        else
        {
            Debug.Log("��������S�ĤH+1");
            enemy = Instantiate(enemyPrefab); // �Ыطs�ĤH
        }

        // ���X�ĤH��A���]��z���סA�T�O�䧹�����z��
        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 1f;  // �]�m�z���׬��������z��
            spriteRenderer.color = color;
        }

        enemy.transform.position = position; // �]�m�ĤH��m
        enemy.SetActive(true); // �ҰʼĤH����

        activeEnemies.Add(enemy); // �T�O�ĤH�Q�l��

        // **�s�W���ˬd�O�_�ݭn�^��**
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
            Debug.LogError("ReturnEnemy2: ���Ϧ^�� null ����I");
            return;
        }

        if (!enemy.activeSelf)
        {
            Debug.LogWarning("ReturnEnemy2: �ĤH�w�g�O�D���ʪ��A�A�����^���C");
            return;
        }

        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy); // �q���D�C�������ĤH
        }

        // �T�O�����٬O active �~�I�s Coroutine
        deleteEnemy deleteScript = enemy.GetComponent<deleteEnemy>();
        if (deleteScript != null && enemy.activeSelf)
        {
            enemy.GetComponent<deleteEnemy>().FadeOutAndReturnToPool(0.15f);  // �H�X�ɶ� // �ĤH�ۤv�B�z Coroutine
        }
        else
        {
            // �����^��
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    public void CheckAndRecycleEnemies()
    {
        List<GameObject> potentialRecycleTargets = new List<GameObject>(); // �Կ�^���C��

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            GameObject enemy = activeEnemies[i];

            if (enemy == null) continue; // ���� MissingReferenceException

            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(enemy.transform.position, detectionRadius, enemyLayer);

            if (nearbyEnemies.Length > 4) // **��o�ӽd�򤺶W�L 4 ��**
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
            // **�H����� 1 ���ĤH�Ӧ^��**
            GameObject enemyToRecycle = potentialRecycleTargets[Random.Range(0, potentialRecycleTargets.Count)];
            Debug.Log($"[CheckAndRecycleEnemies] �^���ĤH: {enemyToRecycle.name}�A��m: {enemyToRecycle.transform.position}");
            ReturnEnemy2(enemyToRecycle);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
