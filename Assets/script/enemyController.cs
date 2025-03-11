using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject enemyPrefab;// �ĤH�w�s��
    public int enemyCount = 5; //�C���Ͳ�5��
    private Vector2 Min;   // �ͦ��d�򪺥��U��
    private Vector2 Max;   // �ͦ��d�򪺥k�W��
    public Transform player; // ���a��m
    public float minDistance = 3f; // �P���a���̤p�Z��

    public float spawnTime= 2f; // �ͦ����j�ɶ��]��^

    void Start(){
        // �}�l�w���ͦ��ĤH
        InvokeRepeating("SpawnEnemies", 0.5f, spawnTime);
        // ��s���
        UpdateBoundaries();
    }

    void SpawnEnemies(){
        int spawnedEnemies = 0; // �w���\�ͦ����ĤH�ƶq

        while (spawnedEnemies < enemyCount){
            Vector2 spawnPosition;
            int attempts = 0;     // ���զ���
            int maxAttempts = 100;// �̤j���զ��ƭ��� �קK�j��
             
            do{
                // �H���ͦ��ĤH��m
                spawnPosition = new Vector2(Random.Range(Min.x, Max.x),
                Random.Range(Min.y, Max.y));

                attempts++;
            }
            while (Vector2.Distance(spawnPosition, player.position) < minDistance && attempts < maxAttempts);

            if (attempts < maxAttempts){
                // �ϥΪ�����qEnemyPool������ĤH
                GameObject enemy = EnemyPool.Instance.GetEnemy(spawnPosition); // �q�����������ĤH
                
                //Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);// �ЫؼĤH
                //(Prefab,�ͦ�����m,�ͦ����ਤ��"�L����")
                
                spawnedEnemies++;
            }
            else{
                Debug.LogWarning("�L�k�b�w���Z���~�ͦ��ĤH");
                break;
            }

            // ��s���
            UpdateBoundaries();
        }
    }


    void UpdateBoundaries()
    {
        // �����v�������
        Camera cam = Camera.main;
        if (cam != null)
        {
            float camHeight = 2f * cam.orthographicSize; // ��v��������
            float camWidth = camHeight * cam.aspect; // ��v�����e��

            // �]�m���
            Min = new Vector2((-camWidth / 2) + 0.37f, (-camHeight / 2) + 0.45f); // ��v��������
            Max = new Vector2((camWidth / 2) - 0.38f, (camHeight / 2) - 2.1f);
        }
    }

    void OnRectTransformDimensionsChange(){ //unity�۱a
        // ��s��� (�����j�p����)
        UpdateBoundaries();
    }
}
