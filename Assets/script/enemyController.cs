using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject enemyPrefab;  //�ĤH�w�s��
    public int enemyCount = 5;      //�C���Ͳ�5��

    private Vector2 Min;            //�ͦ��d�򪺥��U��
    private Vector2 Max;            //�ͦ��d�򪺥k�W��

    public Transform player;        //���a��m
    public float minDistance = 3f;  //�P���a���̤p�Z��

    public float spawnTime= 2f;     //�ͦ����j�ɶ�(��)

    void Start(){

        //�}�l�w���ͦ��ĤH
        InvokeRepeating("SpawnEnemies", 0.5f, spawnTime);

        //��s���
        UpdateBoundaries();
    }

    void SpawnEnemies(){

        //�w���\�ͦ����ĤH�ƶq
        int spawnedEnemies = 0; 

        while (spawnedEnemies < enemyCount){
            Vector2 spawnPosition;

            //���զ���
            int attempts = 0;

            //�̤j���զ��ƭ��� �קK�j��
            int maxAttempts = 100;
             
            do{
                //�H���ͦ��ĤH��m
                spawnPosition = new Vector2(Random.Range(Min.x, Max.x),
                Random.Range(Min.y, Max.y));

                attempts++;
            }
            //���K��player�Ӫ�
            while (Vector2.Distance(spawnPosition, player.position) < minDistance && attempts < maxAttempts);

            //�����EnemyPool������ĤH
            if (attempts < maxAttempts){
                
                //�q�����������ĤH
                GameObject enemy = EnemyPool.Instance.GetEnemy(spawnPosition);
   
                spawnedEnemies++;

            }else{//�L�k�b�w���Z���~�ͦ��ĤH

                break;
            }

            //��s���
            UpdateBoundaries();
        }
    }

    //��s���
    void UpdateBoundaries(){

        //�����v�������
        Camera cam = Camera.main;

        if (cam != null){

            //��v�������� / �e��
            float camHeight = 2f * cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            //�]�m��� (��v��������)
            Min = new Vector2((-camWidth / 2) + 0.37f, (-camHeight / 2) + 0.45f);
            Max = new Vector2((camWidth / 2) - 0.38f, (camHeight / 2) - 2.1f);
        }
    }

    //��s��� (�����j�p����)
    void OnRectTransformDimensionsChange(){ //unity�۱a

        UpdateBoundaries();
    }
}
