using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusControllor : MonoBehaviour{

    public GameObject bonusPrefab;  //�ĤH�w�s��
    public int bonusCount = 2;

    public Vector2 Min;             //�ͦ��d�򪺥��U��
    public Vector2 Max;             //�ͦ��d�򪺥k�W��

    public Transform player;        //���a��m
    public float minDistance = 3f;  //�P���a���̤p�Z��

    public float spawnTime = 10f;   //�ͦ����j�ɶ�(��)
    public float initialDelay = 5f; //��l����ɶ�(��)


    void Start(){

        //�}�l�w���ͦ� (�ͦ��禡,��l����ɶ�,���j�ɶ�)
        InvokeRepeating("SpawnBonus", initialDelay, spawnTime);
    }

    void SpawnBonus(){

        //�w���\�ͦ����ƶq
        int spawnedBonus = 0; 

        //�ͦ�bonus
        while (spawnedBonus < bonusCount){
            Vector2 spawnPosition;

            //���զ���
            int attempts = 0;

            //�̤j���զ��ƭ��� �קK�j��
            int maxAttempts = 100;

            //�H���ͦ���m
            do{ 
                spawnPosition = new Vector2(Random.Range(Min.x, Max.x),
                Random.Range(Min.y, Max.y));

                attempts++;

            }
            //���n�ӱ��񪱮a
            while (Vector2.Distance(spawnPosition, player.position) < minDistance && attempts < maxAttempts);

            if (attempts < maxAttempts){

                //�Ы� (Prefab,�ͦ�����m,�ͦ����ਤ��"�L����")
                Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);
                
                spawnedBonus++;

            }else{ //�L�k�b�w���Z���~�ͦ�

                break;
            }
        }
    }
}
