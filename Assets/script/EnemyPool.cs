using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour{

    public GameObject enemyPrefab;
    public int poolSize = 10;                                         //������j�p

    public float Radius = 2.0f;                                       //�˴��d��
    public Color gizmoColor = Color.red;                              //�ϥ��C��

    private Queue<GameObject> enemyPool = new Queue<GameObject>();    //�x�s�^�����ĤH
    private List<GameObject> activeEnemies = new List<GameObject>();  //�l�ܳ��W���ĤH

    public float detectionRadius = 0.5f;                              //�˴��d��b�|
    public LayerMask enemyLayer;                                      //�M���˴��ĤH���ϼh

    public static EnemyPool Instance;                                 //��ҼҦ�(��K�b��L�a��X��)

    private void Awake(){

        if (Instance == null){

            //EnemyPool��Ҫ�l�Ʀ��\
            Instance = this;

        }else{

            //����ƹ��
            Destroy(gameObject);
        }
        InitializePool();
    }

    //�����
    private void InitializePool(){

        //Enemy Prefab�|���]�m
        if (enemyPrefab == null){
            return;
        }

        //�ЫؼĤH
        for (int i = 0; i < poolSize; i++){

            //�Ы�
            GameObject enemy = Instantiate(enemyPrefab);

            //�]�������
            enemy.SetActive(false);

            //�N�ĤH�[�J�����
            enemyPool.Enqueue(enemy);
        }
    }

    //�q����������ĤH
    public GameObject GetEnemy(Vector3 position){

        GameObject enemy;
        if (enemyPool.Count > 0){

            //���X�ĤH
            enemy = enemyPool.Dequeue(); 

        }else{//��������S�ĤH+1

            //�Ыطs�ĤH
            enemy = Instantiate(enemyPrefab); 
        }

        //���X�ĤH�� ���]��z����
        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null){

            Color color = spriteRenderer.color;

            //�]�m�z���׬��������z��
            color.a = 1f;  
            spriteRenderer.color = color;
        }

        //�]�m�ĤH��m
        enemy.transform.position = position;

        //�ҰʼĤH����
        enemy.SetActive(true);

        //�T�O�ĤH�Q�l��
        activeEnemies.Add(enemy); 

        //�ˬd�O�_�ݭn�^��
        CheckAndRecycleEnemies();

        return enemy;
    }

    //�^���ĤH
    public void ReturnEnemy(GameObject enemy){

            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
    }

    public void ReturnEnemy2(GameObject enemy){

        //�^��null����
        if (enemy == null){

            return;
        }

        //�ĤH�w�g�O�D���ʪ��A �����^��
        if (!enemy.activeSelf){

            return;
        }

        //���D�C�������ĤH
        if (activeEnemies.Contains(enemy)){

            activeEnemies.Remove(enemy); 
        }

        //�T�O�����٬O���ʪ��A�~�I�sCoroutine
        deleteEnemy deleteScript = enemy.GetComponent<deleteEnemy>();

        if (deleteScript != null && enemy.activeSelf){

            //�H�X�ɶ� //�ĤH�ۤv�B�z Coroutine
            enemy.GetComponent<deleteEnemy>().FadeOutAndReturnToPool(0.15f);  

        }else{

            //�����^��
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    //�d�򤺦^���ĤH
    public void CheckAndRecycleEnemies(){

        //�Կ�^���C��
        List<GameObject> potentialRecycleTargets = new List<GameObject>(); 

        for (int i = 0; i < activeEnemies.Count; i++){

            GameObject enemy = activeEnemies[i];

            //����MissingReference
            if (enemy == null) continue; 

            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(enemy.transform.position, detectionRadius, enemyLayer);

            //��o�ӽd�򤺶W�L4��
            if (nearbyEnemies.Length > 4) {

                foreach (Collider2D collider in nearbyEnemies){

                    GameObject targetEnemy = collider.gameObject;
                    if (activeEnemies.Contains(targetEnemy) && !potentialRecycleTargets.Contains(targetEnemy)){

                        potentialRecycleTargets.Add(targetEnemy);
                    }
                }
            }
        }

        //�H����� 1 ���ĤH�Ӧ^��
        if (potentialRecycleTargets.Count > 0){

            //�^���ĤH��m
            GameObject enemyToRecycle = potentialRecycleTargets[Random.Range(0, potentialRecycleTargets.Count)];
            ReturnEnemy2(enemyToRecycle);
        }
    }

    //�i����
    private void OnDrawGizmos(){
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
